using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options
            .WithTheme(ScalarTheme.Kepler)
            .WithDarkModeToggle()
            .WithClientButton());
}

app.MapControllers();

app.Run();