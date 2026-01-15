using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather.Common.Helpers;

public class JsonTextBoolConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.String => ParseHelper.ParseBool(reader.GetString()) ?? false,
            JsonTokenType.Number => ParseHelper.ParseBool(reader.GetUInt64()),
            _ => throw new JsonException(),
        };

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => 
        writer.WriteBooleanValue(value);
}