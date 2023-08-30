using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformOrganizationService
{
    Task<PlatformOrganization> CreateOrganizationAsync(OrganizationName organizationName);
    Task<PlatformOrganization> GetOrganizationAsync(Guid organizationId);
    Task<IEnumerable<PlatformOrganization>> GetAllOrganizationsAsync();
    Task UpdateOrganizationAsync(PlatformOrganization organization);
    Task DeleteOrganizationAsync(Guid organizationId);
}