using System.Linq.Expressions;
using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Identity.InMemoryProvider.Specifications;

public class GetByIdSpecification<TEntity, TId> : ISpecification<TEntity> where TEntity : DomainEntityBase
{
    public TId Id { get; }

    public GetByIdSpecification(TId id)
    {
        Id = id;
    }

    public Expression<Func<TEntity, bool>> Criteria => entity => entity.Id.Equals(Id);

    // No additional includes by default for GetById specification
    public List<Expression<Func<TEntity, object>>> Includes => new List<Expression<Func<TEntity, object>>>();

    // No additional string includes by default for GetById specification
    public List<string> IncludeStrings => new List<string>();

    // No limiting results for GetById as we expect a single entity
    public int Take => -1; 

    // No skipping results for GetById as we expect a single entity
    public int Skip => -1;

    // Paging is not relevant for GetById
    public bool IsPagingEnabled => false;

    // No specific order for GetById as we expect a single entity
    public List<OrderingExpression<TEntity>> OrderBys => new List<OrderingExpression<TEntity>>();

    // No aggregate selector for GetById
    public Expression<Func<TEntity, object>>? AggregateSelector => null;

    // No projection for GetById as we want the full entity
    public Expression<Func<TEntity, object>>? Projection => null;

    public bool DisableTracking => throw new NotImplementedException();

    public bool IgnoreQueryFilters => throw new NotImplementedException();

    // GetById doesn't provide a selector for a specific type by default
    public Expression<Func<TEntity, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }
}

