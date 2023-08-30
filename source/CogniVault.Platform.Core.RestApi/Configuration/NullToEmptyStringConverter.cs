using System.Text.Json;
using System.Text.Json.Serialization;

namespace CogniVault.Platform.Core.RestApi.Configuration;

public class NullToEmptyStringConverter : JsonConverter<string>
{
    // Override default null handling
    public override bool HandleNull => true;

    // Check the type
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string);
    }

    // Ignore for this example
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var value = reader.GetInt32();
            return value.ToString();
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();

            if (value == null) value = string.Empty;
            return value;
        }

        if (reader.TokenType == JsonTokenType.Null) return string.Empty;

        throw new JsonException();
    }

    // 
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteStringValue(string.Empty);
        else
            writer.WriteStringValue(value);
    }
}