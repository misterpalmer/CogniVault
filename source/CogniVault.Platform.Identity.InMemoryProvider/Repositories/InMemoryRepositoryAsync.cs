using System.Collections.Concurrent;
using System.Linq.Expressions;
using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Core.Extensions;
using CogniVault.Platform.Identity.InMemoryProvider.Specifications;

namespace CogniVault.Platform.Identity.InMemoryProvider.Repositories;

public class InMemoryRepositoryAsync<TEntity, TId> : IQueryRepositoryAsync<TEntity, TId>, ICommandRepositoryAsync<TEntity, TId> 
    where TEntity : DomainEntityBase
{
    private readonly ConcurrentDictionary<Guid, TEntity> _store = new ConcurrentDictionary<Guid, TEntity>();

    public async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, decimal>>? selector = null)
    {
        IEnumerable<TEntity> query = _store.Values;

        if (predicate != null)
            query = query.Where(predicate.Compile());

        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return await Task.FromResult(query.Average(selector.Compile()));
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return Task.FromResult(predicate == null ? _store.Count : _store.Values.AsQueryable().Count(predicate));
    }

    public Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        _store.TryRemove((Guid)id, out var _);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _store.TryRemove(entity.Id, out var _);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            _store.TryRemove(entity.Id, out var _);

        return Task.CompletedTask;
    }

    public Task ExecuteSqlCommandAsync(string sql, params object[] parameters)
    {
        throw new NotSupportedException("This operation is not supported on an in-memory repository.");
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null)
    {
        return Task.FromResult(selector == null ? _store.Any() : _store.Values.AsQueryable().Any(selector));
    }

    public Task<IAsyncEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
    {
        var queryable = _store.Values.AsQueryable().Where(spec.Criteria);
        return Task.FromResult(queryable.ToAsyncEnumerable());
    }

    public Task<IAsyncEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<TEntity> spec) where TResult : class
    {
        var queryable = _store.Values.AsQueryable().Where(spec.Criteria);
        
        if (spec.Selector<TResult>() != null)
        {
            return Task.FromResult(queryable.Select(spec.Selector<TResult>().Compile()).ToAsyncEnumerable());
        }

        throw new InvalidOperationException("Selector not provided for TResult projection.");
    }

    public async Task<TEntity> GetByIdAsync(ISpecification<TEntity> spec)
    {
        // Ensure the specification is of the correct type
        if (!(spec is GetByIdSpecification<TEntity, TId>))
            throw new InvalidOperationException("Specification must be of type GetByIdSpecification.");

        var entity = _store.Values.AsQueryable().FirstOrDefault(spec.Criteria);
        return await Task.FromResult(entity);
    }

    public async Task<TResult> GetByIdAsync<TResult>(ISpecification<TEntity> spec) where TResult : class, new()
    {
        // Ensure the specification is of the correct type
        if (!(spec is GetByIdSpecification<TEntity, TId>))
            throw new InvalidOperationException("Specification must be of type GetByIdSpecification.");

        if (spec.Selector<TResult>() == null)
            throw new InvalidOperationException("Selector not provided for TResult projection.");

        var result = _store.Values.AsQueryable().Where(spec.Criteria).Select(spec.Selector<TResult>().Compile()).FirstOrDefault();
        return await Task.FromResult(result);
    }

    public Task<TEntity> GetFirstOrDefaultAsync(ISpecification<TEntity> spec)
    {
        var entity = _store.Values.AsQueryable().FirstOrDefault(spec.Criteria);
        return Task.FromResult(entity);
    }

    public async Task<TResult> GetFirstOrDefaultAsync<TResult>(ISpecification<TEntity> spec) where TResult : class
{
    if (spec.Selector<TResult>() == null)
        throw new InvalidOperationException("Selector not provided for TResult projection.");

    var result = _store.Values.AsQueryable().Where(spec.Criteria).Select(spec.Selector<TResult>().Compile()).FirstOrDefault();
    return await Task.FromResult(result);
}

    public Task<IAsyncEnumerable<TEntity>> GetPagedListAsync(ISpecification<TEntity> spec)
    {
        var queryable = _store.Values.AsQueryable().Where(spec.Criteria).Skip(spec.Skip).Take(spec.Take);
        return Task.FromResult(queryable.ToAsyncEnumerable());
    }

    public Task<IAsyncEnumerable<TResult>> GetPagedListAsync<TResult>(ISpecification<TEntity> spec) where TResult : class, new()
    {
        if (spec.Selector<TResult>() == null)
        {
            throw new InvalidOperationException("Selector not provided for TResult projection.");
        }

        var queryable = _store.Values.AsQueryable()
            .Where(spec.Criteria)
            .Select(spec.Selector<TResult>().Compile())
            .Skip(spec.Skip)
            .Take(spec.Take);

        return Task.FromResult(queryable.ToAsyncEnumerable());
    }

    public Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _store.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            _store.TryAdd(entity.Id, entity);

        return Task.CompletedTask;
    }

    public Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return Task.FromResult(predicate == null ? (long)_store.Count : _store.Values.AsQueryable().LongCount(predicate));
    }

    public Task<T1> MaxAsync<T1>(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, T1>>? selector = default)
    {
        return Task.FromResult(predicate == null ? _store.Values.AsQueryable().Max(selector) : _store.Values.AsQueryable().Where(predicate).Max(selector));
    }

    public Task<T1> MinAsync<T1>(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, T1>>? selector = default)
    {
        return Task.FromResult(predicate == null ? _store.Values.AsQueryable().Min(selector) : _store.Values.AsQueryable().Where(predicate).Min(selector));
    }

    public Task<decimal> SumAsync(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, decimal>>? selector = null)
    {
        return Task.FromResult(predicate == null ? _store.Values.AsQueryable().Sum(selector) : _store.Values.AsQueryable().Where(predicate).Sum(selector));
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // Assuming that the entity with the same ID should replace the old one.
        _store.AddOrUpdate(entity.Id, entity, (_, oldValue) => entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            _store.AddOrUpdate(entity.Id, entity, (_, oldValue) => entity);
        }

        return Task.CompletedTask;
    }

    public Task<int> CompleteAsync()
    {
        return Task.FromResult(0);
    }

    public void Dispose()
    {
        _store.Clear();
    }
}
