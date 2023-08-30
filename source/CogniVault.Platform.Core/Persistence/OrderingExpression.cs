using System.Linq.Expressions;

namespace CogniVault.Platform.Core.Persistence;

public class OrderingExpression<TEntity>
{
    public Expression<Func<TEntity, object>> Expression { get; }
    public bool Descending { get; }

    public OrderingExpression(Expression<Func<TEntity, object>> expression, bool descending = false)
    {
        Expression = expression;
        Descending = descending;
    }
}