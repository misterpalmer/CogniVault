using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;



namespace CogniVault.Platform.Identity.InMemoryProvider.Repositories;

public class OrganizationRepository : InMemoryRepositoryAsync<PlatformOrganization, Guid>, IPlatformOrganizationRepository
{
    public Task<PlatformOrganization?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

}