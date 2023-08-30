using System.Collections.Concurrent;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Stores;

public class OrganizationStoreInMemory : IOrganizationStore
{
    private readonly ConcurrentDictionary<Guid, PlatformOrganization> Organizations = new ConcurrentDictionary<Guid, PlatformOrganization>();
    private readonly ConcurrentDictionary<Guid, PlatformTenant> Tenants = new ConcurrentDictionary<Guid, PlatformTenant>();

    public void AddOrganization(PlatformOrganization organization)
    {
        Organizations.TryAdd(organization.Id, organization);
    }

    public void RemoveOrganization(Guid organizationId)
    {
        Organizations.TryRemove(organizationId, out _);
    }

    public PlatformOrganization GetOrganization(Guid organizationId)
    {
        Organizations.TryGetValue(organizationId, out var organization);
        return organization;
    }

    public void AddTenant(PlatformTenant tenant)
    {
        Tenants.TryAdd(tenant.Id, tenant);
    }

    public void RemoveTenant(Guid tenantId)
    {
        Tenants.TryRemove(tenantId, out _);
    }

    public PlatformTenant GetTenant(Guid tenantId)
    {
        Tenants.TryGetValue(tenantId, out var tenant);
        return tenant;
    }
}