using WebApplicationRabbitMqSignalR.Repositories.Interfaces;

namespace WebApplicationRabbitMqSignalR.Repositories;

// inheritdoc />
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    // inheritdoc />
    public async Task<IEnumerable<WeatherForecast>> Get(string city, DateTime period)
    {
        if (string.IsNullOrEmpty(city))
        {
            return null;
        }
        
        return await Task.FromResult(
            Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
    }
}