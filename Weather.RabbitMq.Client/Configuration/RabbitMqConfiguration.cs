namespace Weather.RabbitMq.Client.Configuration;

/// <summary>
/// RabbitMq Configuration
/// </summary>
public class RabbitMqConfiguration
{
    /// <summary>
    /// Имя хоста
    /// </summary>
    public string HostName { get; set; }
    
    /// <summary>
    /// Имя виртуального хоста
    /// </summary>
    public string VirtualHost { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
   
    /// <summary>
    /// Игнорировать проверку сертификата
    /// </summary>
    public bool CanIgnoreCertificationValidation { get; set; }
    
    /// <summary>
    /// Имя приложения для просмотра в админке
    /// </summary>
    public string ApplicationName { get; set; }
    
    /// <summary>
    /// Порт подключения
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Включает использование SSL
    /// </summary>
    public bool CanEnableSsl { get; set; }
}