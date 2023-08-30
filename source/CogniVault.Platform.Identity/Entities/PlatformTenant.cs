using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformTenant : DomainEntityBase
{
    public Guid OrganizationId { get; private set; }
    public TenantName Name { get; private set; }

    public PlatformTenant(Guid organizationId, IValidator<TenantName> validator, string name)
    {
        OrganizationId = organizationId;
        Name = new TenantName(validator, name);
    }
}