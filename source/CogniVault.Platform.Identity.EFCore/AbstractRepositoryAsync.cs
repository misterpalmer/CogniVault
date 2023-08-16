using System.Linq.Expressions;
using CogniVault.Platform.Core.Collections;
using CogniVault.Platform.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace CogniVault.Platform.Core.Persistence;

public abstract class AbstractRepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : EntityBase<Guid>
{
    private readonly IDbResolver _dbResolver;

    public AbstractRepositoryAsync(IDbResolver dbResolver)
    {
        _dbResolver = dbResolver;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var context = _dbResolver.GetContext<TEntity>();
        context.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.FindAsync<TEntity>(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> expression)
    {
        var context = _dbResolver.GetContext<TEntity>();
        return await context.Set<TEntity>().Where(expression).ToListAsync();
    }

    Task<IList<TEntity>> IRepositoryAsync<TEntity>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(params TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(params TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public void Delete(object id)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(params TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        throw new NotImplementedException();
    }

    public Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false)
    {
        throw new NotImplementedException();
    }

    public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false) where TResult : class
    {
        throw new NotImplementedException();
    }

    public Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        throw new NotImplementedException();
    }

    public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<EntityEntry<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<EntityEntry<TEntity>> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void ChangeEntityState(TEntity entity, EntityState state)
    {
        throw new NotImplementedException();
    }
}