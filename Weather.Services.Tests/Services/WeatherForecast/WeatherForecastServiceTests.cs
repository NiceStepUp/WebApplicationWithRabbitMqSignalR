using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Weather.Common.Models.Configuration;
using Weather.Persistence.Abstractions;
using Weather.RabbitMq.Client.Senders;
using Weather.Services.Services;
using Weather.Services.Tests.Services.WeatherForecast.Auxiliary;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Weather.Services.Tests.Services.WeatherForecast;


[TestClass]
public sealed class WeatherForecastServiceTests
{
    [Theory]
    [MemberData(nameof(WeatherForecastServiceTheoryData.Get), MemberType = typeof(WeatherForecastServiceTheoryData))]
    public async Task TestMethod1(string city, DateTime period, IReadOnlyCollection<Weather.Common.Models.Weather.WeatherForecast> expectedWeatherForecasts)
    {
        // Arrange
        WeatherForecastService weatherForecastService = ArrangeWeatherForecastServiceForSuccess(city, period, expectedWeatherForecasts);

        // Act
        IEnumerable<Weather.Common.Models.Weather.WeatherForecast> actualWeatherForecasts = await weatherForecastService.GetAsync(city, period);

        // Assert
        Assert.IsTrue(expectedWeatherForecasts.SequenceEqual(actualWeatherForecasts));
    }

    private static WeatherForecastService ArrangeWeatherForecastServiceForSuccess(
        string city,
        DateTime period,
        IReadOnlyCollection<Weather.Common.Models.Weather.WeatherForecast> expectedWeatherForecasts)
    {
        Mock<IWeatherForecastRepository> mockWeatherRepository = new();
        mockWeatherRepository
            .Setup(repo => repo.GetAsync(city, period))
            .ReturnsAsync(expectedWeatherForecasts);
        
        IOptions<WeatherConsumerConfiguration> options = Options.Create(new WeatherConsumerConfiguration());
        
        return new(
            mockWeatherRepository.Object,
            new Mock<IRabbitMqMessageSender>().Object,
            new Mock<ILogger<WeatherForecastService>>().Object,
            options);
    }
}