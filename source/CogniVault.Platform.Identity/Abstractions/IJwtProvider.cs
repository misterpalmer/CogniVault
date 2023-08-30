using CogniVault.Platform.Identity.Options;

namespace CogniVault.Platform.Identity.Provider;

public interface IJwtProvider
{
    JwtOptions JwtSettings { get; }
    Task<string> GetJwtAsync(string value);
}