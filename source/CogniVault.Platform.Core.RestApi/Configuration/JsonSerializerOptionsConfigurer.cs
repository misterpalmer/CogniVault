using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

namespace CogniVault.Platform.Core.RestApi.Configuration;

public class JsonSerializerOptionsConfigurer
{
    public static void ConfigureDefaultJsonOptions(JsonOptions jsonOptions)
    {
        jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(SnakeCaseNamingPolicy.Instance));
        jsonOptions.JsonSerializerOptions.Converters.Add(new NullToEmptyStringConverter());
        jsonOptions.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        jsonOptions.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;

        // Use the same serialization settings globally 🌐
        Common.DefaultJsonSerializerOptions = jsonOptions.JsonSerializerOptions;
    }
}