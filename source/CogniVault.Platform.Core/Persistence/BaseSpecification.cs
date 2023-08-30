using System.Linq.Expressions;

using CogniVault.Platform.Core.Abstractions;

namespace CogniVault.Platform.Core.Persistence;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; private set; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;
    public List<OrderingExpression<TEntity>> OrderBys { get; } = new List<OrderingExpression<TEntity>>();
    public Expression<Func<TEntity, object>> AggregateSelector { get; private set; }
    public Expression<Func<TEntity, object>> Projection { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void ApplyOrderBy(OrderingExpression<TEntity> orderBy)
    {
        OrderBys.Add(orderBy);
    }

    protected void ApplyCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void ApplyAggregateSelector(Expression<Func<TEntity, object>> selector)
    {
        AggregateSelector = selector;
    }

    protected void ApplyProjection(Expression<Func<TEntity, object>> projection)
    {
        Projection = projection;
    }

    public Expression<Func<TEntity, TResult>>? Selector<TResult>() where TResult : class
    {
        throw new NotImplementedException();
    }
}
