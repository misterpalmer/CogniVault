using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformUserRepository
{
    Task<PlatformUser<Guid>> GetByIdAsync(Guid id);
    Task<PlatformUser<Guid>> GetByUsernameAsync(Username username);
    Task<bool> IsValidUserCredentialsAsync(Username username, Password password);
    Task AddAsync(PlatformUser<Guid> platformUser);
    Task UpdateAsync(PlatformUser<Guid> platformUser);
    Task DeleteAsync(Guid id);
}