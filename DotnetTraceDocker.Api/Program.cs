using System.Runtime.CompilerServices;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/dosomework", () =>
    {
        var startTime = DateTime.Now;
        
        while (true)
        {
            if ((DateTime.Now - startTime).TotalMilliseconds > 30)
                break;
            
            SomeWork();
        }

        return Results.Ok();
    })
    .WithOpenApi();

app.Run();

[MethodImpl(MethodImplOptions.NoInlining)]
void SomeWork()
{
    var forecast = new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        );

    var json = JsonSerializer.Serialize(forecast);
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
