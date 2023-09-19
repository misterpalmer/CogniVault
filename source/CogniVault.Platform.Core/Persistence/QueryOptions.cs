using System.Linq.Expressions;

namespace CogniVault.Platform.Core.Persistence;
public class QueryOptions<TEntity, TResult>
{
    public Expression<Func<TEntity, TResult>>? Selector { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public CancellationToken CancellationToken { get; set; } = default;
}
