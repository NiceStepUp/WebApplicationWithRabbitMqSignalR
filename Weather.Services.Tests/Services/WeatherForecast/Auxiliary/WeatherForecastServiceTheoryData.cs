namespace Weather.Services.Tests.Services.WeatherForecast.Auxiliary;

/// <summary>
/// Test data
/// </summary>
public static class WeatherForecastServiceTheoryData
{
    public static IEnumerable<object[]> Get
    {
        get
        {
            yield return
            [
                "city_1",
                DateTime.Now,
                new List<Weather.Common.Models.Weather.WeatherForecast>
                {
                    new()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Summary = "Summary_1",
                        TemperatureC = 27
                    }
                }
            ];
            yield return
            [
                "city_2",
                DateTime.Now,
                new List<Weather.Common.Models.Weather.WeatherForecast>
                {
                    new()
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Summary = "Summary_2",
                        TemperatureC = 27
                    }
                }
            ];
        }
    }
}