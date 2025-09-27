using System.Text;
using Dapper;
using FluentMigrator.Runner;
using HighloadSocialNetwork.WebServer.DataAccess;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Repositories;
using HighloadSocialNetwork.WebServer.Services;
using HighloadSocialNetwork.WebServer.Services.Interfaces;
using HighloadSocialNetwork.WebServer.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Scalar.AspNetCore;

var connectionsString = Environment.GetEnvironmentVariable("ConnectionStrings__DbUri")!;
var masterConnectionsString = Environment.GetEnvironmentVariable("ConnectionStrings__MasterDbUri")!;
var jwtKey = Environment.GetEnvironmentVariable("Auth__Jwt_SecretKey")!;

var builder = WebApplication.CreateBuilder(args);

DefaultTypeMap.MatchNamesWithUnderscores = true;
EnsureDatabase(connectionsString, masterConnectionsString);
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(c =>
    {
        c.AddPostgres15_0()
            .WithGlobalConnectionString(connectionsString)
            .ScanIn(typeof(Program).Assembly);
    })
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddSingleton<IDataSourceHolder, DataSourceHolder>()
    .AddSingleton<IDatabaseFactory, DatabaseFactory>()
    .AddTransient(sp => sp.GetRequiredService<IDatabaseFactory>().Create(connectionsString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddSingleton<IAuthRepository, AuthRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme)
            .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme, auth =>
            {
                auth.Token = Environment.GetEnvironmentVariable("DEV_AUTH_TOKEN");
            })
            .WithTitle(nameof(HighloadSocialNetwork.WebServer))
            .WithTheme(ScalarTheme.Kepler)
            .WithDarkModeToggle()
            .WithClientButton();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    UpdateDatabase(scope.ServiceProvider);
}

app.Run();

void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

void EnsureDatabase(string connectionString, string? masterConnectionString)
{
    if (!string.IsNullOrWhiteSpace(masterConnectionString))
    {
        using var dbConnection = new NpgsqlConnection(connectionString);
        using var masterConnection = new NpgsqlConnection(masterConnectionString);

        var dbName = dbConnection.Database;
        var result = masterConnection.ExecuteScalar<int>($"SELECT count(*) FROM pg_database WHERE datname = '{dbName}';");

        if (result == 0)
        {
            masterConnection.Execute($"CREATE DATABASE {dbName};");
        }
    }
}
