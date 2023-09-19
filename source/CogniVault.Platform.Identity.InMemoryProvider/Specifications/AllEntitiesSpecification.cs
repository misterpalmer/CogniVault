using System.Linq.Expressions;
using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Identity.InMemoryProvider.Specifications;

public class AllEntitiesSpecification<T> : ISpecification<T> where T : class
{
    public Expression<Func<T, bool>> Criteria => x => true;

    // Typically, when fetching all entities, you don't want to include any related entities by default. 
    // Clients should explicitly specify when related entities are required.
    public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

    // Similarly, if your ORM supports string-based includes (like Entity Framework), 
    // we won't include any by default.
    public List<string> IncludeStrings => new List<string>();

    // Since this specification is for fetching all entities, Take and Skip 
    // are not applicable. But we'll default them to zero.
    public int Take => 0;
    public int Skip => 0;

    // By default, we're not enabling paging for the AllEntitiesSpecification.
    public bool IsPagingEnabled => false;

    // By default, there's no specific ordering.
    public List<OrderingExpression<T>> OrderBys => new List<OrderingExpression<T>>();

    // No default aggregate selector.
    public Expression<Func<T, object>>? AggregateSelector => null;

    // No default projection.
    public Expression<Func<T, object>>? Projection => null;

    public bool DisableTracking => true;

    public bool IgnoreQueryFilters => false;

    // This method provides a way to project the result into another shape. 
    // By default, we're not doing any special projection.
    public Expression<Func<T, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }
}

