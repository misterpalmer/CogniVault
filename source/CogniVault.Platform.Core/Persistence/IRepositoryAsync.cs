using System.Linq.Expressions;
using CogniVault.Platform.Core.Collections;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace CogniVault.Platform.Core.Persistence;

public interface IRepositoryAsync<TEntity> where TEntity : class
{
    Task<IList<TEntity>> GetAllAsync();

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null);

    Task InsertAsync(params TEntity[] entities);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Update(params TEntity[] entities);

    void Update(IEnumerable<TEntity> entities);

    void Delete(object id);

    void Delete(TEntity entity);

    void Delete(params TEntity[] entities);

    void Delete(IEnumerable<TEntity> entities);

    Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false);

    Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false);

    Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false);

    Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool disableTracking = true,
        CancellationToken cancellationToken = default,
        bool ignoreQueryFilters = false) where TResult : class;

    Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false);

    Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false);

    ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<EntityEntry<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask<EntityEntry<TEntity>> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    public void ChangeEntityState(TEntity entity, EntityState state);
}