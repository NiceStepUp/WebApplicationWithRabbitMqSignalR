using Web.RabbitMq.Client.Configuration;
using Web.RabbitMq.Client.Extensions;
using Web.RabbitMq.Client.Infrastructure;
using Web.RabbitMq.Client.Serialization;
using WebApplicationRabbitMqSignalR.Models.Configuration;
using WebApplicationRabbitMqSignalR.Services;
using WebApplicationRabbitMqSignalR.Services.Interface;
using WebApplicationRabbitMqSignalR.Services.SignalR;
using WebApplicationRabbitMqSignalR.Services.SignalR.Interfaces;
using WebApplicationRabbitMqSignalR.SignalR;
using WebApplicationRabbitMqSignalR.SignalR.Interfaces;

namespace WebApplicationRabbitMqSignalR.Extensions.DependencyInjection;

/// <summary>
/// Расширение для определения DI веб модуля
/// </summary>
public static class WebServiceCollectionExtensions
{
    /// <summary>
    /// Конфигурация фоновых сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureBackgroundHostedServices(
        this IServiceCollection services) =>
        services
            .AddSingleton<IClientContextCommand, ClientContextCommand>()
            .AddHostedService<WeatherHostedService>();

    /// <summary>
    /// Конфигурация сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddSingleton<IWeatherSubscriberManager, WeatherSubscriberManager>();
        
        return services;
    }
    
    /// <summary>
    /// Конфигурация фоновых сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureWebModule(
        this IServiceCollection services) =>
        services
            .AddSingleton<IRabbitMqCorrelationIdContext, RabbitMqCorrelationIdContext>();

    /// <summary>
    /// Конфигурация фоновых сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services.Configure<RabbitMqConfiguration>(configuration.GetSection(nameof(RabbitMqConfiguration)))
            .Configure<WeatherConsumerConfiguration>(configuration.GetSection(nameof(WeatherConsumerConfiguration)))
            .AddSingleton<IRabbitMqJsonMessageSerializer, RabbitMqJsonMessageSerializer>()
            .AddRabbitMq();
    
    /// <summary>
    /// Конфигурация сервисов 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param> 
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureAppSettings(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherHostedServiceSettings>(configuration.GetSection(nameof(WeatherHostedServiceSettings)));
        return services;
    }
}