

using CogniVault.Platform.Core.Persistence;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.EFCoreProvider.Specifications;

public class GetByNameSpecification : BaseSpecification<PlatformOrganization>
{
    public GetByNameSpecification(OrganizationName name)
    {
        ApplyCriteria(entity => entity.Name.Equals(name));
    }
}