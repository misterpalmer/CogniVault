using System.Text.Json;
using System.Text.Json.Serialization;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Converters;


public class DirectoryNameJsonConverter : JsonConverter<DirectoryName>
{
    public override DirectoryName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ((DirectoryName) reader.GetString()).Copy();
    }

    public override void Write(Utf8JsonWriter writer, DirectoryName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
