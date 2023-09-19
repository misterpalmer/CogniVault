using System.Linq.Expressions;

using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Persistence;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.InMemoryProvider.Specifications;

public class GetByUsernameSpecification<TEntity, TId> : ISpecification<PlatformUser>
{
    public Username Username { get; }

    public GetByUsernameSpecification(Username username)
    {
        Username = username;
    }

    public Expression<Func<PlatformUser, bool>> Criteria => user => user.Username.Value.Equals(Username.Value);

    // You can include other properties of PlatformUser that you may want to eagerly load
    public List<Expression<Func<PlatformUser, object>>> Includes => new List<Expression<Func<PlatformUser, object>>>();

    // No additional string includes by default for GetByUsername specification
    public List<string> IncludeStrings => new List<string>();

    // Limiting results for GetByUsername as we expect a single entity (or none)
    public int Take => 1;

    // No skipping results for GetByUsername as we expect a single entity
    public int Skip => 0;

    // Paging is not relevant for GetByUsername
    public bool IsPagingEnabled => false;

    // No specific order for GetByUsername as we expect a single entity
    public List<OrderingExpression<PlatformUser>> OrderBys => new List<OrderingExpression<PlatformUser>>();

    // No aggregate selector for GetByUsername
    public Expression<Func<PlatformUser, object>>? AggregateSelector => null;

    // No projection for GetByUsername as we want the full entity
    public Expression<Func<PlatformUser, object>>? Projection => null;

    public bool DisableTracking => true;

    public bool IgnoreQueryFilters => false;

    // GetByUsername doesn't provide a selector for a specific type by default
    public Expression<Func<PlatformUser, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }
}

