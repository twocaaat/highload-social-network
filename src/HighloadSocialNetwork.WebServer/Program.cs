using FluentMigrator.Runner;
using HighloadSocialNetwork.WebServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetupServerServices()
    .AddServices();

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
