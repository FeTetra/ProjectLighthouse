using LBPUnion.ProjectLighthouse.Middlewares;

namespace LBPUnion.ProjectLighthouse.Servers.Presence.Startup;

public class PresenceTestStartup : PresenceStartup
{
    public PresenceTestStartup(IConfiguration configuration) : base(configuration)
    {}

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<FakeRemoteIPAddressMiddleware>();
        base.Configure(app, env);
    }
}