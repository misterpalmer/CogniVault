using System.Text.Json;
using CogniVault.Platform.Core.Extensions;

namespace CogniVault.Platform.Core.RestApi.Configuration;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new();

    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}