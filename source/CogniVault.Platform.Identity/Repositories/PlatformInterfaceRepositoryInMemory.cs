using System.Collections.Concurrent;

using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Attributes;
using CogniVault.Platform.Identity.Constants;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Repositories;

[IdentityRepositoryProvider(IdentityRepositoryProviderKeyConstants.InMemory)]
public class PlatformInterfaceRepositoryInMemory : IPlatformInterfaceRepository
{
    private readonly ConcurrentDictionary<Guid, PlatformInterface> _store = new();

    public Task AddInterfaceAsync(PlatformInterface interfaceEntity)
    {
        if (!_store.TryAdd(interfaceEntity.Id, interfaceEntity))
            throw new ArgumentException("An interface with the same ID already exists.");

        return Task.CompletedTask;
    }

    public Task UpdateInterfaceAsync(PlatformInterface interfaceEntity)
    {
        // If the item doesn't exist in the store, an exception will be thrown.
        if (!_store.ContainsKey(interfaceEntity.Id))
            throw new ArgumentException("No interface with the given ID exists.");

        _store[interfaceEntity.Id] = interfaceEntity;
        return Task.CompletedTask;
    }

    public Task DeleteInterfaceAsync(Guid interfaceId)
    {
        _store.TryRemove(interfaceId, out _);
        return Task.CompletedTask;
    }

    public Task<PlatformInterface> GetInterfaceAsync(Guid interfaceId)
    {
        _store.TryGetValue(interfaceId, out var interfaceEntity);
        return Task.FromResult(interfaceEntity);
    }

    public Task<IEnumerable<PlatformInterface>> GetAllInterfacesForTenantAsync(Guid tenantId)
    {
        // Assuming PlatformInterface has a TenantId property to link it to a specific tenant.
        var interfaces = _store.Values.Where(i => i.TenantId == tenantId);
        return Task.FromResult(interfaces.AsEnumerable());
    }

    public Task<PlatformInterface?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}