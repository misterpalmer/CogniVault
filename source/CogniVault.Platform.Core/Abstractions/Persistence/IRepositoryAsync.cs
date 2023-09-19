using System.Linq.Expressions;
using CogniVault.Platform.Core.Collections;
using CogniVault.Platform.Core.Persistence;
using Microsoft.EntityFrameworkCore;


namespace CogniVault.Platform.Core.Abstractions.Persistence;

public interface IRepositoryAsync<TEntity> where TEntity : class
{
    Task<object> QueryAsync<TResult>(
        ISpecification<TEntity> specification,
        QueryOptions<TEntity, TResult> options = null,
        CancellationToken cancellationToken = default) where TResult : class;
    
    Task<IList<TResult>> GetAllAsync<TResult>(
        ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>>? selector = null,
        CancellationToken cancellationToken = default) where TResult : class;

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null);

    TEntity? Find(params object[] keyValues);

    ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void Update(IEnumerable<TEntity> entities);

    void Delete(IEnumerable<TEntity> entities);

    public void ChangeEntityState(TEntity entity, EntityState state);
}
