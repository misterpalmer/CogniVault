using System.Collections.Concurrent;

using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Attributes;
using CogniVault.Platform.Identity.Constants;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Repositories;

[IdentityRepositoryProvider(IdentityRepositoryProviderKeyConstants.InMemory)]
public class PlatformOrganizationRepositoryInMemory : IPlatformOrganizationRepository
{
    private readonly ConcurrentDictionary<Guid, PlatformOrganization> _store = new ConcurrentDictionary<Guid, PlatformOrganization>();

    public Task AddOrganizationAsync(PlatformOrganization organization)
    {
        if (_store.ContainsKey(organization.Id))
            throw new ArgumentException("An organization with the same ID already exists.");

        _store[organization.Id] = organization;
        return Task.CompletedTask;
    }

    public Task UpdateOrganizationAsync(PlatformOrganization organization)
    {
        if (!_store.ContainsKey(organization.Id))
            throw new ArgumentException("No organization with the given ID exists.");

        _store[organization.Id] = organization;
        return Task.CompletedTask;
    }

    public Task DeleteOrganizationAsync(Guid organizationId)
    {
        _store.TryRemove(organizationId, out _);
        return Task.CompletedTask;
    }

    public Task<PlatformOrganization> GetOrganizationAsync(Guid organizationId)
    {
        _store.TryGetValue(organizationId, out var organization);
        return Task.FromResult(organization);
    }

    public Task<IEnumerable<PlatformOrganization>> GetAllOrganizationsAsync()
    {
        return Task.FromResult(_store.Values.AsEnumerable());
    }

    public Task<PlatformOrganization?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}