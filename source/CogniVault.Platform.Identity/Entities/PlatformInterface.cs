using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Entities;

public class PlatformInterface : DomainEntityBase
{
    public Guid OrganizationId { get; private set; }
    public Guid TenantId { get; private set; }
    public InterfaceName Name { get; private set; }

    public PlatformInterface(IValidator<InterfaceName> validator, string name)
    {
        Name = new InterfaceName(validator, name);
    }
}