using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Builders;

public class PlatformOrganizationBuilder : IEntityBuilder<PlatformOrganization>
{
    public Task<PlatformOrganization> BuildAsync()
    {
        throw new NotImplementedException();
    }
}



// public class Builder
// {
//     public OrganizationName Name { get; private set; }
//     public Uri? LogoUri { get; private set; }
//     public IList<PlatformTenant> Tenants { get; private set; } = new List<PlatformTenant>();

//     public Builder(OrganizationName name)
//     {
//         Name = name;
//     }

//     public Builder(OrganizationName name, Uri uri)
//     {
//         Name = name;
//         LogoUri = uri;
//     }

//     public Task<Builder> AddTenantAsync(PlatformTenant tenant)
//     {
//         Tenants.Add(tenant);
//         return this;
//     }

//     public async Task<PlatformOrganization> BuildAsync()
//     {
//         await ValidateDataAsync();

//         var org = new PlatformOrganization(Name, LogoUri);
//         foreach (var tenant in Tenants)
//         {
//             org.AddTenant(tenant);
//         }

//         return org;
//     }

//     private async Task ValidateDataAsync()
//     {
//         // var validator = new OrganizationNameValidator();
//         // var result = await validator.ValidateAsync(Name);
//         // if (!result.IsValid)
//         // {
//         //     throw new InvalidOperationException("Invalid Name");
//         // }

//         if (Name == null)
//         {
//             throw new InvalidOperationException("Name must be set.");
//         }
//         if (Tenants.Count == 0)
//         {
//             throw new InvalidOperationException("At least one Tenant must be added.");
//         }

//         return new PlatformOrganization(this);
//     }
// }