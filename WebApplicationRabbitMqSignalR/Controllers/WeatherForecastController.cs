using Microsoft.AspNetCore.Mvc;
using WebApplicationRabbitMqSignalR.Services.Interface;

namespace WebApplicationRabbitMqSignalR.Controllers;

[ApiController]
[Route("weather-forecast")]
public class WeatherForecastController : ControllerBase
{
    
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService; 

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> Get(string? cityName, DateTime period) =>
        await _weatherForecastService.Get(cityName, period);
}
