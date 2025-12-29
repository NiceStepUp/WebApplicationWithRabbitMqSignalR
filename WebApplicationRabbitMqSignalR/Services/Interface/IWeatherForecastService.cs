namespace WebApplicationRabbitMqSignalR.Services.Interface;
/// <summary>
/// Abstraction of weather service layer
/// </summary>
public interface IWeatherForecastService
{
    /// <summary>
    /// Getting weather fot city
    /// </summary>
    /// <param name="city">City</param>
    /// <param name="period"></param>
    /// <returns>Weather forecast for city</returns>
    Task<IEnumerable<WeatherForecast>> Get(string city, DateTime period);
}