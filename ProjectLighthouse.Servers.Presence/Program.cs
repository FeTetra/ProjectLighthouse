using LBPUnion.ProjectLighthouse;
using LBPUnion.ProjectLighthouse.Configuration;
using LBPUnion.ProjectLighthouse.Logging.Loggers.AspNet;
using LBPUnion.ProjectLighthouse.Servers.Presence.Startup;
using LBPUnion.ProjectLighthouse.Types.Misc;

await StartupTasks.Run(ServerType.Presence);

IHostBuilder builder = Host.CreateDefaultBuilder();
builder.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<PresenceStartup>();
    webBuilder.UseUrls(ServerConfiguration.Instance.PresenceListenUrl);
});

builder.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddProvider(new AspNetToLighthouseLoggerProvider());
});

await builder.Build().RunAsync();