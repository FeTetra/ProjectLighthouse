using LBPUnion.ProjectLighthouse.Configuration;
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Middlewares;
using LBPUnion.ProjectLighthouse.Types.Entities.Profile;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Middlewares;

public class EmailEnforcementMiddleware : MiddlewareDBContext
{
    private static bool requireVerifiedEmails = ServerConfiguration.Instance.Mail.RequireVerifiedEmails;

    private static readonly HashSet<string> pathWhitelist = new()
    {
        "login",
        "eula",
        "announce",
        "notification",
    };

    public EmailEnforcementMiddleware(RequestDelegate next) : base(next)
    { }

    public override async Task InvokeAsync(HttpContext context, DatabaseContext database)
    {
        if (!requireVerifiedEmails)
        {
            await this.next(context);
            return;
        }

        GameTokenEntity? token = await database.GameTokenFromRequest(context.Request);
        UserEntity? user = await database.UserFromGameToken(token);
        if (user == null)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Not a valid user");
            return;
        }

        string[] pathSegments = context.Request.Path.ToString().Split("/", StringSplitOptions.RemoveEmptyEntries);

        // Continue with non-whitelisted paths if user's email address is valid and verified
        if (!pathWhitelist.Contains(pathSegments[1]) && user.EmailAddressVerified && user.EmailAddress != null)
        {
            await this.next(context);
            return;
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync("User has invalid email address");
        return;
    }
}
