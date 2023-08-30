using System.Security.Claims;

using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface ITokenService
{
    Task<string> GenerateTokenForUserAsync(Username username);
    ClaimsPrincipal ValidateToken(string token);
}
