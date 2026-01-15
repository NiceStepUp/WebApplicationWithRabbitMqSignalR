using Weather.Common.Models.Weather;
using Weather.Persistence.Abstractions;

namespace Weather.Persistence.Repositories;

// inheritdoc />
public class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    // inheritdoc />
    public async Task<IEnumerable<WeatherForecast>> GetAsync(string city, DateTime period) =>
        await Task.FromResult( // We mocked this method by mock data, but here should be database call. 
            Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
}