using System.Text;
using RabbitMQ.Client.Events;

namespace Weather.RabbitMq.Client.Extensions;

/// <summary>
/// Расширения для аргументов события доставки сообщения
/// </summary>
public static class BasicDeliveryEventArgsExtensions
{
    /// <summary>
    /// Получает значение заголовка
    /// </summary>
    /// <param name="e">Аргументы события</param>
    /// <param name="headerName">Имя заголовка</param>
    /// <returns>Значение заголовка</returns>
    public static string GetHeaderValue(this BasicDeliverEventArgs e, string headerName)
    {
        string headerValue = 
            e.BasicProperties.Headers == null
            || !e.BasicProperties.Headers.TryGetValue(headerName, out object header)
                ? string.Empty
                : Encoding.UTF8.GetString((byte[])header);
            
        return headerValue;
    }
    
}