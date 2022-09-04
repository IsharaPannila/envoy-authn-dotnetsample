var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/health", () => "alive");

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 1).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
});

// Add Pathbase routing for AKS hosting
var pathBase = builder.Configuration["PATH_BASE"];

if (!string.IsNullOrEmpty(pathBase))
{
    app.UsePathBase(pathBase);
    app.UseRouting(); //https://github.com/dotnet/AspNetCore.Docs/pull/25769
}

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}