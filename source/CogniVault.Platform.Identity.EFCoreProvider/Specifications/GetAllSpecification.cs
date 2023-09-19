using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Identity.EFCoreProvider.Specifications;

public class GetAllSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : DomainEntityBase
{
    public GetAllSpecification() : base()
    {
        
    }
}