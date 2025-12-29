using WebApplicationRabbitMqSignalR.Models.SignalR;

namespace WebApplicationRabbitMqSignalR.Services.SignalR.Interfaces;

/// <summary>
/// Добавляет, удаляет, получает подписчиков для получения уведомлений
/// об обновлениях данных погоды для SignalR
/// </summary>
public interface IWeatherSubscriberManager
{
    /// <summary>
    /// Добавляет подписчика или обновляет данные подписчика
    /// </summary>
    /// <param name="weatherSubscription">Данные о подписке</param>
    void AddOrUpdate(WeatherSubscription weatherSubscription);
    
    /// <summary>
    /// Удаляет подписчика
    /// </summary>
    /// <param name="connectionId">Идентификатор соединения</param>
    /// <returns>true if an object was removed successfully; otherwise, false</returns>
    bool Remove(string connectionId);
    
    /// <summary>
    /// Получение всех подписчиков
    /// </summary>
    /// <returns>Все подписчики</returns>
    IEnumerable<WeatherSubscription> GetAll();
}