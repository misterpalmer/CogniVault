using System.Text.Json;
using System.Text.Json.Serialization;

using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Converters;


public class InterfaceNameJsonConverter : JsonConverter<InterfaceName>
{
    public override InterfaceName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ((InterfaceName) reader.GetString()).Copy();
    }

    public override void Write(Utf8JsonWriter writer, InterfaceName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
