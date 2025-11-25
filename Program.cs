using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);

try
{
    DotNetEnv.Env.Load();
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddScoped<HealthCheckPort, HealthCheckAdapter>();
    builder.Host.UseNLog();

    var app = builder.Build();

    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    await app.RunAsync();

}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw new InvalidOperationException("An error occurred while starting the application.", ex);

}
finally
{
    LogManager.Shutdown();
}
