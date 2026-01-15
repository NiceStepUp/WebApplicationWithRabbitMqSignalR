using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Weather.Common.Models.Weather;
using Weather.Services.Abstractions.Interface;

namespace Weather.WebApplicationRabbitMqSignalR.Controllers;

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
    public async Task<IEnumerable<WeatherForecast>> GetAsync(
        [Required(AllowEmptyStrings = false)] string cityName,
        DateTime period) =>
        await _weatherForecastService.GetAsync(cityName, period);
}
