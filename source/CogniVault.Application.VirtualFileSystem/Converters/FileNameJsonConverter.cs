using System.Text.Json;
using System.Text.Json.Serialization;

using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Converters;


public class FileNameJsonConverter : JsonConverter<FileName>
{
    public override FileName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ((FileName) reader.GetString()).Copy();
    }

    public override void Write(Utf8JsonWriter writer, FileName value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
