using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformUserService
{
    Task<PlatformUser> GetByUsernameAsync(Username username);
    Task<bool> IsValidUserCredentialsAsync(Username username, PlainPassword password);
}