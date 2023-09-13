using System.Linq.Expressions;

using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetAllFromNodeSpecification<T> : ISpecification<T> where T : FileSystemNode
{
    protected readonly Guid ParentId;

    public GetAllFromNodeSpecification(Guid parentId)
    {
        this.ParentId = parentId;
    }

    public Expression<Func<T, bool>> Criteria => node => node.Parent.Id == ParentId;

    public List<Expression<Func<T, object>>> Includes => new List<Expression<Func<T, object>>>();

    public List<string> IncludeStrings => new List<string>();

    public int Take => 0;

    public int Skip => 0;

    public bool IsPagingEnabled => false;

    public List<OrderingExpression<T>> OrderBys => new List<OrderingExpression<T>>();

    public Expression<Func<T, object>>? AggregateSelector => null;

    public Expression<Func<T, object>>? Projection => null;

    public Expression<Func<T, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }
}