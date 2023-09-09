using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;
namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformOrganization
{
    // OrganizationName Name { get; }
    // Uri? LogoUri { get; }
    // IEnumerable<PlatformTenant> TenantsReadOnly { get; }
    // IEnumerable<PlatformInterface> Interfaces { get; }
    // DateTimeOffset DisabledOnUtc { get; }
    // bool IsEnabled { get; }

    // CRUD Operations for Tenants
    Task AddTenantAsync(PlatformTenant tenant);
    Task RemoveTenantAsync(PlatformTenant tenant);

    // CRUD Operations for Interfaces
    Task AddInterfaceAsync(PlatformTenant tenant, PlatformInterface platformInterface);
    Task RemoveInterfaceAsync(PlatformTenant tenant, PlatformInterface platformInterface);

    // Activation Operations
    Task ActivateAsync();
    Task DeactivateAsync();

    // Asynchronous Operations
    Task UpdateNameAsync(OrganizationName name);
    Task UpdateLogoUriAsync(Uri uri);

    /// <summary>
    /// Retrieves an organization from the repository using its name.
    /// </summary>
    /// <param name="name">The name of the organization to retrieve.</param>
    /// <returns>The organization if found; otherwise, null.</returns>
    Task<PlatformOrganization?> GetByNameAsync(OrganizationName name);
}