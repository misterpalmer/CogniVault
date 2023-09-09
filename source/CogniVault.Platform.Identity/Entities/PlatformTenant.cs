using System.Reflection;
using System.Text.Json.Serialization;

using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Builders;
using CogniVault.Platform.Identity.Converters;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformTenant : DomainEntityBase, IPlatformTenant
{
    [JsonIgnore] public PlatformOrganization Organization { get; private set; }
    [JsonConverter(typeof(TenantNameJsonConverter))] public TenantName Name { get; private set; }
    public string TenantIdentifier { get; private set; } = string.Empty;
    public string ConnectionString { get; private set; } = string.Empty;
    public string AdminEmail { get; private set; } = string.Empty;
    public Uri? LogoUri { get; private set; }
    public bool IsEnabled { get; private set; } = true;
    public bool IsDefault { get; private set; } = false;
    public DateTimeOffset DisabledOnUtc { get; private set; } = DateTimeOffset.Now.AddMonths(1); // DEMO period
    [JsonIgnore] public IList<PlatformInterface> Interfaces { get; private set; } = new List<PlatformInterface>();
    [JsonPropertyName("Interfaces")] public IEnumerable<PlatformInterface> InterfacesReadOnly => Interfaces.AsReadOnly();
    [JsonIgnore] public bool IsNullObject => this.Id == Guid.Empty;
    // Null Object Pattern implementation:
    public static PlatformTenant Null => new PlatformTenant(PlatformOrganization.Null, TenantName.Null)
    {
        Id = Guid.Empty
    };

    public PlatformTenant(PlatformTenantBuilder builder)
    {
        Organization = builder.Organization; // Note: Property name adjusted for consistency
        Name = builder.Name; // Note: Property name adjusted for consistency
        Interfaces = builder.Interfaces; // Note: Property name adjusted for consistency

        InitializeCommonProperties();
    }

    protected PlatformTenant(in PlatformOrganization organization, in TenantName name)
    {
        Organization = organization;
        Name = name.Copy();

        InitializeCommonProperties();
    }

    public static async Task<PlatformTenant> CreateAsync(PlatformOrganization organization, TenantName name)
    {
        await Task.Delay(1);
        return new PlatformTenant(organization, name);
    }

    public void AddInterface(PlatformInterface platformInterface)
    {
        Interfaces.Add(platformInterface);
    }
    
    public void SetValidityPeriod(in DateTimeOffset disabledOnUtc)
    {
        DisabledOnUtc = disabledOnUtc
        > DateTimeOffset.UtcNow ? disabledOnUtc : DateTimeOffset.UtcNow.AddMonths(1);
        UpdateCommonProperties();
    }

    public void AddValidityPeriod(int months)
    {
        DisabledOnUtc = DisabledOnUtc.AddMonths(months);
        UpdateCommonProperties();
    }

    public async Task ActivateAsync()
    {
        await Task.Delay(1); // Simulating async operation
        Activate();
    }

    public async Task DeactivateAsync()
    {
        await Task.Delay(1); // Simulating async operation
        Deactivate();
    }
    
    public void Activate()
    {
        IsEnabled = true;
        UpdateCommonProperties();
    }

    public void Deactivate()
    {
        IsEnabled = false;
        UpdateCommonProperties();
    }

    public async Task AddInterfaceAsync(PlatformInterface platformInterface)
    {
        await Task.Delay(1); // Simulating async operation
        AddInterface(platformInterface);
    }

    public async Task RemoveInterfaceAsync(PlatformInterface platformInterface)
    {
        await Task.Delay(1); // Simulating async operation
        Interfaces.Remove(platformInterface);
    }

    public async Task<PlatformTenant?> GetByTenantNameAsync(TenantName name)
    {
        await Task.Delay(1);
        return null;
    }

    private void InitializeCommonProperties()
    {
        Id = Guid.NewGuid();

        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string assemblyName = typeof(PlatformTenant).FullName ?? "System";
        
        ModifiedOnUtc = CreatedOnUtc = currentTimestamp;
        ModifiedBy = CreatedBy = assemblyName;
    }
    
    private void UpdateCommonProperties()
    {
        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string typeName = typeof(PlatformTenant).FullName ?? "System";

        ModifiedOnUtc = currentTimestamp;
        ModifiedBy = typeName;
    }

}




