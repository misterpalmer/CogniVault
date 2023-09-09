using System.Reflection;
using System.Text.Json.Serialization;

using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Converters;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformOrganization : DomainEntityBase, IPlatformOrganization, IAggregateRoot
{
    [JsonConverter(typeof(OrganizationNameJsonConverter))]
    public OrganizationName Name { get; private set; }
    public Uri? LogoUri { get; private set; }
    public bool IsEnabled { get; private set; } = true;
    [JsonIgnore] public IList<PlatformTenant> Tenants { get; private set; } = new List<PlatformTenant>();
    [JsonPropertyName("Tenants")] public IEnumerable<PlatformTenant> TenantsReadOnly => Tenants.AsReadOnly();
    // public IEnumerable<PlatformInterface> Interfaces => Tenants.SelectMany(t => t.Interfaces).ToList().AsReadOnly();

    [JsonIgnore] public bool IsNullObject => this.Id == Guid.Empty;

    protected PlatformOrganization(OrganizationName name)
    {
        Name = name.Copy();
        InitializeCommonProperties();
    }

    protected PlatformOrganization(OrganizationName name, Uri uri) : this(name)
    {
        if (uri == null)
            throw new ArgumentNullException(nameof(uri));

        LogoUri = uri;
    }

    // Null Object Pattern implementation:
    public static PlatformOrganization Null => new PlatformOrganization(OrganizationName.Null)
    {
        Id = Guid.Empty
    };

    public static async Task<PlatformOrganization> CreateAsync(OrganizationName name)
    {
        await Task.Delay(1);
        return new PlatformOrganization(name);
    }

    public static async Task<PlatformOrganization> CreateAsync(OrganizationName name, Uri uri)
    {
        await Task.Delay(1);
        return new PlatformOrganization(name, uri);
    }

    public async Task AddTenantAsync(PlatformTenant tenant)
    {
        await Task.Delay(1);
        if (tenant == PlatformTenant.Null)
            throw new ArgumentNullException(nameof(tenant));

        Tenants.Add(tenant);
        UpdateCommonProperties();
    }

    public async Task RemoveTenantAsync(PlatformTenant tenant)
    {
        await Task.Delay(1);
        if (tenant == null)
            throw new ArgumentNullException(nameof(tenant));

        Tenants.Remove(tenant);
        UpdateCommonProperties();
    }

    public async Task AddInterfaceAsync(PlatformTenant tenant, PlatformInterface platformInterface)
    {
        await Task.Delay(1);
        if (tenant == null)
            throw new InvalidOperationException("Invalid Tenant");

        if (platformInterface == null)
            throw new ArgumentNullException(nameof(platformInterface));

        tenant.AddInterface(platformInterface);
    }

    public async Task RemoveInterfaceAsync(PlatformTenant tenant, PlatformInterface platformInterface)
    {
        if (tenant == PlatformTenant.Null)
            throw new InvalidOperationException("Invalid Tenant");

        if (platformInterface == null)
            throw new ArgumentNullException(nameof(platformInterface));

        await tenant.RemoveInterfaceAsync(platformInterface);
    }

    public async Task ActivateAsync()
    {
        await Task.Delay(1);
        // TODO Activate default tenant and default interfaces
        IsEnabled = true;
        UpdateCommonProperties();
    }

    public async Task DeactivateAsync()
    {
        await Task.Delay(1);
        // TODO Deactivate tenats and interfaces
        IsEnabled = false;
        UpdateCommonProperties();
    }

    public async Task UpdateNameAsync(OrganizationName newName)
    {
        await Task.Delay(1);
        Name = newName.Copy();
        UpdateCommonProperties();
    }

    public async Task UpdateLogoUriAsync(Uri uri)
    {
        await Task.Delay(1);
        LogoUri = uri;
        UpdateCommonProperties();
    }

    public async Task<PlatformOrganization?> GetByNameAsync(OrganizationName name)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    private void InitializeCommonProperties()
    {
        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string typeName = typeof(PlatformOrganization).FullName ?? "System";

        Id = Guid.NewGuid();
        ModifiedOnUtc = CreatedOnUtc = currentTimestamp;
        ModifiedBy = CreatedBy = typeName;
    }

    private void UpdateCommonProperties()
    {
        DateTimeOffset currentTimestamp = DateTimeOffset.UtcNow;
        string typeName = typeof(PlatformOrganization).FullName ?? "System";

        ModifiedOnUtc = currentTimestamp;
        ModifiedBy = typeName;
    }
}
