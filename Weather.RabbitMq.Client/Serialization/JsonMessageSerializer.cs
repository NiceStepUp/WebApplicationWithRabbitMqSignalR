using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather.RabbitMq.Client.Serialization;


/// <inheritdoc cref="IRabbitMqJsonMessageSerializer"/>
public class RabbitMqJsonMessageSerializer : IRabbitMqJsonMessageSerializer
{
    private static JsonSerializerOptions _jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };
    
    /// <inheritdoc /> 
    public string Serialize(object message) => 
        JsonSerializer.Serialize(message, _jsonSerializerOptions);

    /// <inheritdoc />
    public T Deserialize<T>(string message) => 
      JsonSerializer.Deserialize<T>(message, _jsonSerializerOptions);
}