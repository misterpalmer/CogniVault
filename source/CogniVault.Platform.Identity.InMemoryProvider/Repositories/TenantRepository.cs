using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.InMemoryProvider.Repositories;

public class TenantRepository : InMemoryRepositoryAsync<PlatformTenant, Guid>, IPlatformTenantRepository
{
    public Task<PlatformTenant?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}