using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformTenant
{
    PlatformOrganization Organization { get; }
    TenantName Name { get; }
    Task<PlatformTenant?> GetByTenantNameAsync(TenantName name);
    Task AddInterfaceAsync(PlatformInterface platformInterface);
    Task RemoveInterfaceAsync(PlatformInterface platformInterface);
    void SetValidityPeriod(in DateTimeOffset disabledOnUtc);
    void AddValidityPeriod(int months);
    void Activate();
    void Deactivate();
}