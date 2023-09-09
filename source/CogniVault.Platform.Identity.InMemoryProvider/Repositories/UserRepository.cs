using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.InMemoryProvider.Repositories;

public class UserRepository : InMemoryRepositoryAsync<PlatformUser, Guid>
    , IPlatformUserRepository<PlatformUser>
{
    public Task<PlatformUser> GetByUsernameAsync(Username username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsValidUserCredentialsAsync(Username username, EncryptedPassword password)
    {
        throw new NotImplementedException();
    }

    Task<PlatformUser?> IPlatformUserRepository<PlatformUser>.GetByUsernameAsync(Username username)
    {
        throw new NotImplementedException();
    }
}
