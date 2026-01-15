using System.Collections.Concurrent;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather.Common.Helpers;

/// <summary>
/// Параметры для Json сериализатора
/// </summary>
public sealed class JsonTextOptions
{

    private static readonly JsonTextOptions _instance = new();

    private static readonly ConcurrentDictionary<JsonNamingPolicyParameters, JsonSerializerOptions>
        _jsonSerializerOptionsCache = new();
    private static JsonSerializerOptions _jsonSerializerCamelCaseOptions;
    private static JsonSerializerOptions _jsonSerializerDefaultOptions;

    /// <summary>
    /// <see cref="JsonTextOptions"/>
    /// </summary>
    public static JsonTextOptions Instance => _instance;

    /// <summary>
    /// Возвращает параметры сериализатора в camelCase для значений идентификаторов (напр.: nameParameter) /// </summary>
    public JsonSerializerOptions CamelCaseOptions =>
        _jsonSerializerCamelCaseOptions =
            _jsonSerializerCamelCaseOptions ?? GetOptions(JsonNamingPolicy.CamelCase);

    /// <summary>
    /// Возвращает параметры сериализатора по умолчанию для значений идентификаторов (напр.: NameParameter) /// </summary>
    public JsonSerializerOptions DefaultOptions =>
        _jsonSerializerDefaultOptions = _jsonSerializerDefaultOptions ?? GetOptions();

    ///<summary>
    /// Возвращает параметры сериализатора с возможностью настройки именования идентификаторов и форматирования /// </summary>
    /// <param name="jsonNamingPolicy">Нaстраивает политику именований идентиикаторов, нaпp. camelCase</param>
    /// <param name="writeIndented">булевое значение, tгuе: орматирование json-a, false: без дорматирования</param>
    /// <param name="ignoreNullCondition">Булевое значение, true: сериализовать null значения свойств, false: не сериализовать null значен /// <returns>Возвращает параметры json сериализатора</returns>
    public JsonSerializerOptions GetOptions(
        JsonNamingPolicy jsonNamingPolicy = null,
        bool writeIndented = false,
        bool ignoreNullCondition = true)
    {
        JsonNamingPolicyParameters cacheKey = new JsonNamingPolicyParameters
        {
            JsonNamingPolicy = jsonNamingPolicy,
            WriteIndented = writeIndented,
            IgnoreNullCondition = ignoreNullCondition
        };
        
        return _jsonSerializerOptionsCache.GetOrAdd(cacheKey, newValue =>
        {
            JsonSerializerOptions jsonSerializerOptions = new()
            {
                DefaultIgnoreCondition = ignoreNullCondition
                    ? JsonIgnoreCondition.WhenWritingNull
                    : JsonIgnoreCondition.Never,
                PropertyNamingPolicy = jsonNamingPolicy,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true,
                WriteIndented = writeIndented,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
           
            jsonSerializerOptions.Converters.Add(new JsonTextDecimalConverter());
            jsonSerializerOptions.Converters.Add(new JsonTextBoolConverter());
            return jsonSerializerOptions;
        });
    }

}