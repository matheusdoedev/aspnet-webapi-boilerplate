using Microsoft.AspNetCore.Authentication.JwtBearer;
using NLog;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;

Env.Load();

string tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? throw new ArgumentException("token key env not defined");
string tokenIssuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER") ?? throw new ArgumentException("token key env not defined");
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? throw new ArgumentException("db connection string env not defined");
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Logger logger = LogManager.GetCurrentClassLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<HealthCheckPort, HealthCheckAdapter>();
builder.Services.AddScoped<AuthenticationPort, AuthenticationAdapter>();
builder.Services.AddScoped<UserRepositoryPort, UserRepositoryAdapter>();
builder.Services.AddScoped<TokenizerPort, TokenizerAdapter>();
builder.Services.AddScoped<EncryptorPort, EncryptorAdapter>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey))
        };
    });
builder.Services.AddAuthorization();

try
{
    WebApplication app = builder.Build();

    app.UseAuthorization();
    app.UseAuthentication();
    app.MapOpenApi();
    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped because of an exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}