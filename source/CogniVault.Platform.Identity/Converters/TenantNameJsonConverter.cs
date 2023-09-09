using System.Text.Json;
using System.Text.Json.Serialization;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Converters;

public class TenantNameJsonConverter : JsonConverter<TenantName>
{
    public override TenantName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ((TenantName) reader.GetString()).Copy();
    }

    public override void Write(Utf8JsonWriter writer, TenantName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}