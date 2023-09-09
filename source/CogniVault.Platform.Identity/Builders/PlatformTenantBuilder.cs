using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Builders;

public class PlatformTenantBuilder : IEntityBuilder<PlatformTenant>
{
    private readonly List<Func<Task>> _asyncTasks = new List<Func<Task>>();
    public PlatformOrganization Organization { get; private set; } = PlatformOrganization.Null;
    public TenantName Name { get; private set; } = TenantName.Null;
    public IList<PlatformInterface> Interfaces { get; private set; } = new List<PlatformInterface>(); // Note: Made it public for use in PlatformTenant


    public PlatformTenantBuilder()
    {
        _asyncTasks.Add(async () => await Task.Delay(1));
    }

    public PlatformTenantBuilder WithOrganization(PlatformOrganization organization)
    {
        Organization = organization;
        _asyncTasks.Add(async () => await ValidateOrganizationAsync(Organization));
        return this;
    }

    public PlatformTenantBuilder WithTenantName(TenantName name)
    {
        Name = name.Copy();
        _asyncTasks.Add(async () => await ValidateTenantNameAsync(Name));
        return this;
    }

    public async Task<PlatformTenantBuilder> AddInterfaceAsync(PlatformInterfaceBuilder interfaceBuilder)
    {
        var platformInterface = await interfaceBuilder.BuildAsync();
        Interfaces.Add(platformInterface);
        return this;
    }

    public async Task<PlatformTenant> BuildAsync()
    {
        foreach (var task in _asyncTasks)
        {
            await task();
        }

        var platformTenant = await PlatformTenant.CreateAsync(Organization, Name);

        return platformTenant;
    }

    private async Task ValidateOrganizationAsync(PlatformOrganization organization)
    {
        await Task.Delay(1); // Simulate some async validation, e.g., database lookup

        if (organization == PlatformOrganization.Null)
        {
            throw new InvalidOperationException("Invalid organization");
        }
    }

    private async Task ValidateTenantNameAsync(TenantName name)
    {
        await Task.Delay(1); // Simulate some async validation, e.g., database lookup
        if (name == TenantName.Null)
        {
            throw new InvalidOperationException("Invalid tenant name");
        }
    }
}


// public class Builder
// {
//     public Guid OrganizationId { get; private set; }
//     public TenantName Name { get; private set; }
//     public IList<PlatformInterface> Interfaces { get; private set; } = new List<PlatformInterface>();

//     public Builder(Guid organizationId, TenantName name)
//     {
//         OrganizationId = organizationId;
//         Name = name;
//     }

//     public Builder AddInterface(PlatformInterface platformInterface)
//     {
//         Interfaces.AddInterface(platformInterface);
//         return this;
//     }

//     public async Task<PlatformTenant> BuildAsync()
//     {
//         if (OrganizationId == Guid.Empty)
//         {
//             throw new InvalidOperationException("OrganizationId must be set.");
//         }
//         if (Interfaces.Count == 0)
//         {
//             throw new InvalidOperationException("At least one Interface must be added.");
//         }

//         return new PlatformTenant(this);
//     }
// }