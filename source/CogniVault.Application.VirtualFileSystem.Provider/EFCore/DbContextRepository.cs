using System.Linq.Expressions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CogniVault.Application.VirtualFileSystem.Provider.EFCore;

public class DbContextRepositoryAsync<T> : IQueryRepositoryAsync<T>, ICommandRepositoryAsync<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public DbContextRepositoryAsync(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = dbContext.Set<T>();
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>>? selector = null)
    {
        if (selector == null)
        {
            return await _dbSet.AnyAsync();
        }

        return await _dbSet.AnyAsync(selector, cancellationToken: default);
    }

    public async Task<IAsyncEnumerable<T>> GetAllAsync(ISpecification<T> spec)
    {
        return _dbSet.Where(spec.Criteria).AsAsyncEnumerable();
    }

    public async Task<IAsyncEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<T> spec) where TResult : class
    {
        // Assuming you have a way to map from T to TResult
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(ISpecification<T> spec)
    {
        return await _dbSet.Where(spec.Criteria).FirstOrDefaultAsync();
    }

    public async Task<TResult> GetByIdAsync<TResult>(ISpecification<T> spec) where TResult : class, new()
    {
        // Assuming you have a way to map from T to TResult
        throw new NotImplementedException();
    }

    public async Task<T> GetFirstOrDefaultAsync(ISpecification<T> spec)
    {
        return await _dbSet.Where(spec.Criteria).FirstOrDefaultAsync();
    }

    public async Task<TResult> GetFirstOrDefaultAsync<TResult>(ISpecification<T> spec) where TResult : class
    {
        // Assuming you have a way to map from T to TResult
        throw new NotImplementedException();
    }

    public async Task<IAsyncEnumerable<T>> GetPagedListAsync(ISpecification<T> spec)
    {
        // Assuming you have a way to paginate
        throw new NotImplementedException();
    }

    public async Task<IAsyncEnumerable<TResult>> GetPagedListAsync<TResult>(ISpecification<T> spec) where TResult : class, new()
    {
        // Assuming you have a way to paginate and map from T to TResult
        throw new NotImplementedException();
    }

    public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
