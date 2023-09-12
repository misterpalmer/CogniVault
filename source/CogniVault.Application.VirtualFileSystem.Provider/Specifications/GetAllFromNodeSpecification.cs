using System.Linq.Expressions;

using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetAllFromNodeSpecification<T> : ISpecification<FileSystemNode> where T : class
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

    Expression<Func<FileSystemNode, bool>> ISpecification<FileSystemNode>.Criteria => throw new NotImplementedException();

    List<Expression<Func<FileSystemNode, object>>> ISpecification<FileSystemNode>.Includes => throw new NotImplementedException();

    List<OrderingExpression<FileSystemNode>> ISpecification<FileSystemNode>.OrderBys => throw new NotImplementedException();

    Expression<Func<FileSystemNode, object>>? ISpecification<FileSystemNode>.AggregateSelector => throw new NotImplementedException();

    Expression<Func<FileSystemNode, object>>? ISpecification<FileSystemNode>.Projection => throw new NotImplementedException();

    // This method provides a way to project the result into another shape. 
    // By default, we're not doing any special projection.
    public Expression<Func<T, TResult>>? Selector<TResult>() where TResult : class
    {
        return null;
    }

    Expression<Func<FileSystemNode, TResult>>? ISpecification<FileSystemNode>.Selector<TResult>()
    {
        throw new NotImplementedException();
    }
}

