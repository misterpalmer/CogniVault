using System.Reflection;
using System.Text.Json.Serialization;

using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Converters;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformOrganization : DomainEntityBase
{
    [JsonConverter(typeof(OrganizationNameJsonConverter))]
    public OrganizationName Name { get; private set; }

    [JsonIgnore]
    public bool IsNullObject => this.Id == Guid.Empty;

    public PlatformOrganization(OrganizationName name)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));

        Name = name.Copy();
        InitializeCommonProperties();
    }

    public static async Task<PlatformOrganization> CreateAsync(string name, IValidator<OrganizationName> validator)
    {
        var organizationName = await OrganizationName.CreateAsync(name, validator);
        return new PlatformOrganization(organizationName);
    }


    private void InitializeCommonProperties()
    {
        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string assemblyName = Assembly.GetAssembly(typeof(PlatformOrganization)).FullName ?? "System";

        Id = Guid.NewGuid();
        ModifiedOnUtc = CreatedOnUtc = currentTimestamp;
        ModifiedBy = CreatedBy = assemblyName;
    }

    private void UpdateCommonProperties()
    {
        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string assemblyName = Assembly.GetAssembly(typeof(PlatformOrganization)).FullName ?? "System";

        ModifiedOnUtc = currentTimestamp;
        ModifiedBy = assemblyName;
    }

    // Null Object Pattern implementation:
    public static PlatformOrganization Null => new PlatformOrganization(OrganizationName.Null)
    {
        Id = Guid.Empty
    };


    // Update the organization's name
    public async Task UpdateNameAsync(string newName, IValidator<OrganizationName> validator)
    {
        var updatedName = await OrganizationName.CreateAsync(newName, validator);
        if (updatedName == null)
            throw new ArgumentNullException(nameof(updatedName));

        Name = updatedName;
    }
}
