using System.Text.Json;

namespace WebApplicationRabbitMqSignalR.Helpers;

/// <summary>
/// Helper class for Json
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// Converts the provided value into a string.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>A string representation of the value.</returns>
    public static string Serialize(object value, JsonSerializerOptions? options = null) =>
        JsonSerializer.Serialize(value, options);
    
    /// <summary>
    /// Converts the provided value into a string.
    /// </summary>
    /// <param name="json">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>A string representation of the value.</returns>
    public static TValue Deserialize<TValue>(string json, JsonSerializerOptions? options = null) =>
        JsonSerializer.Deserialize<TValue>(json, options ?? JsonTextOptions.Instance.CamelCaseOptions);
    
    
}