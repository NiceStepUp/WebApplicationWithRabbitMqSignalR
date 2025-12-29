using System.Text.Json;

namespace WebApplicationRabbitMqSignalR.Helpers;

/// <summary>
/// Параметры для политики маппинга наименований в json
/// </summary>
public class JsonNamingPolicyParameters
{
    /// <summary>
    /// see <see cref="JsonNamingPolicy"/>
    /// </summary>
    public JsonNamingPolicy JsonNamingPolicy { get; set; }

    /// <summary>
    /// see <see cref="JsonSerializerOptions.WriteIndented"/>
    /// </summary>
    public bool WriteIndented { get; set; }
    
    /// <summary>
    /// see <see cref="JsonSerializerOptions.DefaultIgnoreCondition"/>
    /// </summary>
    public bool IgnoreNullCondition { get; set; }

    protected bool Equals(JsonNamingPolicyParameters other) =>
        JsonNamingPolicy.Equals(other.JsonNamingPolicy)
        && WriteIndented == other.WriteIndented
        && IgnoreNullCondition == other.IgnoreNullCondition;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((JsonNamingPolicyParameters)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine(JsonNamingPolicy, WriteIndented, IgnoreNullCondition);
}