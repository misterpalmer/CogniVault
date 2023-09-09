using CogniVault.Platform.Identity.Builders;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Extensions;

public static class PlatformOrganizationExtensions
{
    public static async Task<PlatformOrganization> AddTenantAsync(this PlatformOrganization organization, PlatformTenantBuilder tenantBuilder)
    {
        if (organization == null)
            throw new ArgumentNullException(nameof(organization));

        if (tenantBuilder == null)
            throw new ArgumentNullException(nameof(tenantBuilder));

        var tenant = await tenantBuilder.WithOrganization(organization).BuildAsync();
        organization.Tenants.Add(tenant);
        return organization;
    }

    public static async Task<PlatformOrganization> AddTenantAsync(this PlatformOrganization organization, PlatformTenant tenant)
    {
        await Task.Delay(1); // If there's no real async work to be done, consider removing the Task.Delay and making the method synchronous.
        
        if (organization == null)
            throw new ArgumentNullException(nameof(organization));

        if (tenant == null)
            throw new ArgumentNullException(nameof(tenant));

        organization.Tenants.Add(tenant);
        return organization;
    }
}