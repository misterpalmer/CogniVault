using System.Collections.Concurrent;

using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Attributes;
using CogniVault.Platform.Identity.Constants;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Repositories;

[IdentityRepositoryProvider(IdentityRepositoryProviderKeyConstants.InMemory)]
public class PlatformTenantRepositoryInMemory : IPlatformTenantRepository
{
    private readonly ConcurrentDictionary<Guid, PlatformInterface> _interfaceStore = new ConcurrentDictionary<Guid, PlatformInterface>();

    public Task AddInterfaceAsync(PlatformInterface interfaceEntity)
    {
        if (_interfaceStore.ContainsKey(interfaceEntity.Id))
            throw new ArgumentException("An interface with the same ID already exists.");

        _interfaceStore[interfaceEntity.Id] = interfaceEntity;
        return Task.CompletedTask;
    }

    public Task UpdateInterfaceAsync(PlatformInterface interfaceEntity)
    {
        if (!_interfaceStore.ContainsKey(interfaceEntity.Id))
            throw new ArgumentException("No interface with the given ID exists.");

        _interfaceStore[interfaceEntity.Id] = interfaceEntity;
        return Task.CompletedTask;
    }

    public Task DeleteInterfaceAsync(Guid interfaceId)
    {
        _interfaceStore.TryRemove(interfaceId, out _);
        return Task.CompletedTask;
    }

    public Task<PlatformInterface> GetInterfaceAsync(Guid interfaceId)
    {
        _interfaceStore.TryGetValue(interfaceId, out var interfaceEntity);
        return Task.FromResult(interfaceEntity);
    }

    public Task<IEnumerable<PlatformInterface>> GetAllInterfacesForTenantAsync(Guid tenantId)
    {
        // Assuming PlatformInterface has a TenantId property to link it to a specific tenant.
        var interfaces = _interfaceStore.Values.Where(i => i.Id == tenantId);
        return Task.FromResult(interfaces.AsEnumerable());
    }

    public Task<PlatformTenant?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}