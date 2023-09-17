using System.Linq.Expressions;

using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Core.Abstractions;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    // Add Selector property
    Expression<Func<TEntity, TResult>>? Selector<TResult>() where TResult : class;
    List<string> IncludeStrings { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    List<OrderingExpression<TEntity>> OrderBys { get; }
    Expression<Func<TEntity, object>>? AggregateSelector { get; }
    Expression<Func<TEntity, object>>? Projection { get; }
    bool DisableTracking { get; }
    bool IgnoreQueryFilters { get; }
}