using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather.Common.Helpers;

public class JsonTextDecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return reader.TokenType == JsonTokenType.String
                ? TryParse(reader)
                : reader.GetDecimal();
        }
        catch (InvalidOperationException e)
        {
            return TryParse(reader);
        }
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value);

    private decimal TryParse(Utf8JsonReader reader) =>
        decimal.TryParse(
            reader.GetString(),
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out decimal result)
            ? result
            : 0;
}