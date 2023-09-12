using System.Linq.Expressions;
using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetDirectoryContentsSpecification<T> : ISpecification<T> where T : FileSystemNode
{
    public Guid Id { get; }

    public GetDirectoryContentsSpecification(Guid id)
    {
        Id = id;
    }

    public Expression<Func<T, bool>> Criteria => entity => entity.Parent.Id.Equals(Id);

    public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

    public List<string> IncludeStrings => new List<string>();

    public int Take => -1;

    public int Skip => -1;

    public bool IsPagingEnabled => false;

    public List<OrderingExpression<T>> OrderBys => new List<OrderingExpression<T>>();

    public Expression<Func<T, object>>? AggregateSelector => null;

    public Expression<Func<T, object>>? Projection => null;

    public Expression<Func<T, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }
}

