using System.Text.Json;
using System.Text.Json.Serialization;

using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Identity.Converters;

// public class ValueObjectJsonConverter<TValueObject, T> : JsonConverter<TValueObject> 
//     where TValueObject : IValueObject<T>
// {
//     public override TValueObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//     {
//         var value = reader.GetString();
//         return (TValueObject)Activator.CreateInstance(typeof(TValueObject), new object[] { value });
//     }

//     public override void Write(Utf8JsonWriter writer, TValueObject value, JsonSerializerOptions options)
//     {
//         writer.WriteStringValue(value.Value.ToString());
//     }
// }
