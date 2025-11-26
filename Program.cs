using NLog;
using NLog.Web;

using Microsoft.EntityFrameworkCore;

Logger? logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? throw new ArgumentException("Invalid connection string.");

try
{
    DotNetEnv.Env.Load();
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    builder.Services.AddScoped<HealthCheckPort, HealthCheckAdapter>();
    builder.Services.AddScoped<AuthenticationPort, AuthenticationAdapter>();
    builder.Host.UseNLog();

    WebApplication app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.MapControllers();
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
