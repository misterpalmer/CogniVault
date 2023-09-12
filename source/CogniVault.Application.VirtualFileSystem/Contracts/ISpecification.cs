using System.Linq.Expressions;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface ISpecification<T> where T : class
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    // Add Selector property
    Expression<Func<T, TResult>>? Selector<TResult>() where TResult : class;
    List<string> IncludeStrings { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    List<OrderingExpression<T>> OrderBys { get; }
    Expression<Func<T, object>>? AggregateSelector { get; }
    Expression<Func<T, object>>? Projection { get; }
}