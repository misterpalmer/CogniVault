using System.Collections.Concurrent;

using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IOrganizationStore
{
    void AddOrganization(PlatformOrganization organization);
    void RemoveOrganization(Guid organizationId);
    PlatformOrganization GetOrganization(Guid organizationId);

    void AddTenant(PlatformTenant tenant);
    void RemoveTenant(Guid tenantId);
    PlatformTenant GetTenant(Guid tenantId);
}
