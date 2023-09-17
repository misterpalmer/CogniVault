using System.Reflection;
using System.Text.Json.Serialization;

using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Converters;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformInterface : DomainEntityBase
{
    public Guid TenantId { get; private set; }
    [JsonIgnore] public PlatformTenant Tenant { get; private set; } = PlatformTenant.Null;
    [JsonConverter(typeof(InterfaceNameJsonConverter))] public InterfaceName Name { get; private set; }
    public string InterfaceIdentifier { get; private set; } = string.Empty;
    public string ConnectionString { get; private set; } = string.Empty;
    public string AdminEmail { get; private set; } = string.Empty;
    public Uri? LogoUri { get; private set; }
    public bool IsEnabled { get; private set; } = true;
    public bool IsDefault { get; private set; } = false;
    public DateTimeOffset DisabledOnUtc { get; private set; } = DateTimeOffset.Now.AddMonths(1); // DEMO period
    [JsonIgnore] public bool IsNullObject => this.Id == Guid.Empty;
    // Null Object Pattern implementation:
    public static PlatformInterface Null => new PlatformInterface(InterfaceName.Null)
    {
        Id = Guid.Empty
    };

    public PlatformInterface()
    {
        InitializeCommonProperties();
    }
    protected PlatformInterface(InterfaceName name)
    {
        Name = name.Copy();
    }

    protected PlatformInterface(PlatformTenant tenant, InterfaceName name)
    {
        TenantId = tenant.Id;
        Tenant = tenant;
        Name = name.Copy();
        InitializeCommonProperties();
    }

    protected PlatformInterface(PlatformTenant tenant,
        InterfaceName name,
        string identifier,
        string connectionString,
        string adminEmail,
        Uri? logoUri,
        bool isEnabled,
        bool isDefault,
        DateTimeOffset? disabledOnUtc)
    {
        TenantId = tenant.Id;
        Tenant = tenant;
        Name = name.Copy();
        InterfaceIdentifier = identifier;
        ConnectionString = connectionString;
        AdminEmail = adminEmail;
        LogoUri = logoUri;
        IsEnabled = isEnabled;

        InitializeCommonProperties();
    }

    public static async Task<PlatformInterface> CreateAsync(PlatformTenant tenant, InterfaceName name)
    {
        await Task.Delay(1);
        return new PlatformInterface(tenant, name);
    }

    public static async Task<PlatformInterface> CreateAsync(PlatformTenant tenant, InterfaceName name, string identifier, string connectionString, string adminEmail, Uri? logoUri, bool isEnabled, bool isDefault, DateTimeOffset? disabledOnUtc)
    {
        await Task.Delay(1);
        return new PlatformInterface(tenant, name, identifier, connectionString, adminEmail, logoUri, isEnabled, isDefault, disabledOnUtc);
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

// error messages
// Subscription can 't be backdated.
// Invalid Tenant
// Id can't be null
// Identifier can't be null
// ConnectionString can't be null
// AdminEmail can't be null
// LogoUri can't be null

