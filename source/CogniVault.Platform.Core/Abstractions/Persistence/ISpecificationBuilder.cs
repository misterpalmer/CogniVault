using System.Linq.Expressions;

using CogniVault.Platform.Core.Persistence;

namespace CogniVault.Platform.Core.Abstractions;

public interface ISpecificationBuilder<TEntity>
{
    ISpecificationBuilder<TEntity> WithCriteria(Expression<Func<TEntity, bool>> criteria);
    ISpecificationBuilder<TEntity> WithInclude(Expression<Func<TEntity, object>> includeExpression);
    ISpecificationBuilder<TEntity> WithInclude(string includeString);
    ISpecificationBuilder<TEntity> WithOrdering(OrderingExpression<TEntity> orderBy);
    ISpecificationBuilder<TEntity> WithPaging(int skip, int take);
    ISpecificationBuilder<TEntity> WithAggregateSelector(Expression<Func<TEntity, object>> aggregateSelector);
    ISpecificationBuilder<TEntity> WithProjection(Expression<Func<TEntity, object>> projection);
    ISpecification<TEntity> Build();
}