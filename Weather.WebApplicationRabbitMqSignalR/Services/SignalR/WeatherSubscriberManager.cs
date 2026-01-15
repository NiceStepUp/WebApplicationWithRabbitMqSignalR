using System.Collections.Concurrent;
using Weather.WebApplicationRabbitMqSignalR.Models.SignalR;
using Weather.WebApplicationRabbitMqSignalR.Services.SignalR.Interfaces;

namespace Weather.WebApplicationRabbitMqSignalR.Services.SignalR;

/// <summary>
/// CRUD - менеджер для подписок с фронта на уведомления от SignalR в C#
/// </summary>
public class WeatherSubscriberManager : IWeatherSubscriberManager
{
    private readonly ConcurrentDictionary<string, WeatherSubscription> _weatherSubscribers = new();

    /// <inheritdoc />
    public void AddOrUpdate(WeatherSubscription weatherSubscription) =>
        _weatherSubscribers.AddOrUpdate(
            weatherSubscription.ConnectionId,
            weatherSubscription,
            (_, oldValue) => weatherSubscription);

    /// <inheritdoc />
    public bool Remove(string connectionId) => 
        _weatherSubscribers.TryRemove(connectionId, out _);

    /// <inheritdoc />
    public IEnumerable<WeatherSubscription> GetAll() =>
        _weatherSubscribers.Select(s => s.Value);
}