using System.Linq.Expressions;

namespace CogniVault.Platform.Core.Persistence;

// public class Specification<TEntity>
// {
//     public Expression<Func<TEntity, bool>>? Criteria { get; set; }
//     public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();
//     public List<string> IncludeStrings { get; } = new List<string>();
//     public List<OrderingExpression<TEntity>> OrderBys { get; } = new List<OrderingExpression<TEntity>>();
//     public int Take { get; private set; }
//     public int Skip { get; private set; }
//     public bool IsPagingEnabled { get; private set; } = false;

//     // Add any other required specifications...

//     public void ApplyPaging(int skip, int take)
//     {
//         Skip = skip;
//         Take = take;
//         IsPagingEnabled = true;
//     }
// }

