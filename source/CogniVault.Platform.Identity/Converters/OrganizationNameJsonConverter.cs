using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Converters;
public class OrganizationNameJsonConverter : JsonConverter<OrganizationName>
{
    public override OrganizationName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ((OrganizationName) reader.GetString()).Copy();
    }

    public override void Write(Utf8JsonWriter writer, OrganizationName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
