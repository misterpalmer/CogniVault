using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.InMemoryProvider.Repositories;

public class InterfaceRepository : InMemoryRepositoryAsync<PlatformInterface, Guid>, IPlatformInterfaceRepository
{
    public Task<PlatformInterface?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
