namespace WebApplicationRabbitMqSignalR.Repositories.Interfaces;

/// <summary>
/// Abstraction of weather repository layer
/// </summary>
public interface IWeatherForecastRepository
{
    /// <summary>
    /// Getting weather for city
    /// </summary>
    /// <param name="city">City</param>
    /// <param name="period"></param>
    /// <returns>Weather forecast for city</returns>
    Task<IEnumerable<WeatherForecast>> Get(string city, DateTime period);
}