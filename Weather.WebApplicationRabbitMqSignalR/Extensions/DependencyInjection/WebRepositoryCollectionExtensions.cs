using Weather.Persistence.Abstractions;
using Weather.Persistence.Repositories;

namespace Weather.WebApplicationRabbitMqSignalR.Extensions.DependencyInjection;

/// <summary>
/// Расширение для определения DI веб модуля
/// </summary>
public static class WebRepositoryCollectionExtensions
{
    /// <summary>
    /// Конфигурация фоновых сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureRepositories(
        this IServiceCollection services) =>
        services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
}