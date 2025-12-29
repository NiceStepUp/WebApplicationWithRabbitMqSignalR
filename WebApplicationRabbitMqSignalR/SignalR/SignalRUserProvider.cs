using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace WebApplicationRabbitMqSignalR.SignalR;

/// <summary>
/// Поставщик идентификатора пользователя для рассылки сообщений
/// конкретным пользователям через SignalR
/// </summary>
public class SignalRUserProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection) => 
        connection.User?.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value
            ?? connection.User?.Identity?.Name;
}