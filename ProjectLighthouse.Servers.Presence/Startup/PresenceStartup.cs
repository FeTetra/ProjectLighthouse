using LBPUnion.ProjectLighthouse.Configuration;
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Middlewares;
using LBPUnion.ProjectLighthouse.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LBPUnion.ProjectLighthouse.Servers.Presence.Startup;

public class PresenceStartup
{
    public PresenceStartup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddMvc
        (
            options =>
            {
                options.OutputFormatters.Add(new JsonOutputFormatter());
            }
        );

        services.AddDbContext<DatabaseContext>(builder =>
        {
            builder.UseMySql(ServerConfiguration.Instance.DbConnectionString,
                MySqlServerVersion.LatestSupportedServerVersion);
        });
    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        #if DEBUG
        app.UseDeveloperExceptionPage();
        #endif

        app.UseMiddleware<RequestLogMiddleware>();

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}