using CogniVault.Platform.Core.RestApi.Configuration;

namespace CogniVault.Platform.Identity.Provider;

public interface IJwtProvider
{
    JwtOptions JwtSettings { get; }
    Task<string> GetJwtAsync(string value);
}