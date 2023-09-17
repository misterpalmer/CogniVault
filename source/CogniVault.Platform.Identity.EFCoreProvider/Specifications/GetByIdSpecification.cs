using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Identity.EFCoreProvider.Specifications;

public class GetByIdSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : DomainEntityBase
{
    public GetByIdSpecification(Guid id) : base()
    {
        ApplyCriteria(entity => entity.Id.Equals(id));
    }
}