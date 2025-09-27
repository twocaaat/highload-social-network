using System.Reflection;
using System.Text;
using Dapper;
using FluentMigrator.Runner;
using FluentValidation;
using HighloadSocialNetwork.WebServer.DataAccess;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace HighloadSocialNetwork.WebServer.Extensions;

public static class ServerSetupExtensions
{
    public static IServiceCollection SetupServerServices(this IServiceCollection services)
    {
        services.AddDatabase();
        services.AddAuth();
        
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
        
        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        return services;
    }

    private static void AddDatabase(this IServiceCollection services)
    {
        var connectionsString = Environment.GetEnvironmentVariable("ConnectionStrings__DbUri")!;
        var masterConnectionsString = Environment.GetEnvironmentVariable("ConnectionStrings__MasterDbUri")!;
        
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        EnsureDatabase(connectionsString, masterConnectionsString);
        services.AddFluentMigratorCore()
            .ConfigureRunner(c =>
            {
                c.AddPostgres15_0()
                    .WithGlobalConnectionString(connectionsString)
                    .ScanIn(typeof(Program).Assembly);
            })
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        services.AddSingleton<IDataSourceHolder, DataSourceHolder>()
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddTransient(sp => sp.GetRequiredService<IDatabaseFactory>().Create(connectionsString));
    }

    private static void AddAuth(this IServiceCollection services)
    {
        var jwtKey = Environment.GetEnvironmentVariable("Auth__Jwt_SecretKey")!;
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
    }
    
    private static void EnsureDatabase(string connectionString, string? masterConnectionString)
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
}