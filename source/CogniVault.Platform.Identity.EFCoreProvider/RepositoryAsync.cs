using System.Linq.Expressions;

using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Collections;
using CogniVault.Platform.Core.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryAsync(DbContext context)
    {
        Context = context;
        _dbSet = Context.Set<TEntity>();
    }

    private IQueryable<TEntity> ApplySpecification(BaseSpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = _dbSet;

        query = SpecificationEvaluator<TEntity>.GetQuery(query, specification);

        return query;
    }

    public async Task<object> QueryAsync<TResult>(
        ISpecification<TEntity> specification,
        QueryOptions<TEntity, TResult> options = null,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var query = SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);

        if (options?.Selector != null)
        {
            return await query.Select(options.Selector)
                              .ToListAsync(options.CancellationToken);
        }

        // Assumes TEntity is compatible with TResult.
        // You should add checks to ensure that this cast is valid.
        return await query.Cast<TResult>()
                          .ToListAsync(options.CancellationToken);
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>(
        ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>>? selector = null,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var query = SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);

        if (selector != null)
        {
            return await query.Select(selector).ToListAsync(cancellationToken);
        }

        // Assumes TEntity is compatible with TResult.
        // You should add checks to ensure that this cast is valid.
        var results = await query.Cast<TResult>().ToListAsync(cancellationToken);
        return results;
    }
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null)
    {
        if (selector == null)
            return await _dbSet.AnyAsync();
        return await _dbSet.AnyAsync(selector);
    }

    public TEntity? Find(params object[] keyValues)
    {
        return _dbSet.Find(keyValues);
    }

    public async ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(keyValues, cancellationToken);
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        return;
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }

    public void ChangeEntityState(TEntity entity, EntityState state)
    {
        Context.Entry(entity).State = state;
    }
}
