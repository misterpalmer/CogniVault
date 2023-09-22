using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Platform.Core.Extensions;

namespace CogniVault.Application.VirtualFileSystem.Provider.Memory;

public class MemoryRepositoryAsync<T> : IQueryRepositoryAsync<T>, ICommandRepositoryAsync<T> where T : FileSystemNode
{
    private readonly ConcurrentDictionary<Guid, T> _store = new ConcurrentDictionary<Guid, T>();
    
    public Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>>? selector = null)
    {
        throw new NotImplementedException();
    }

    public Task<IAsyncEnumerable<T>> GetAllAsync(ISpecification<T> spec)
    {
        var queryable = _store.Values.AsQueryable().Where(spec.Criteria);
        return Task.FromResult(queryable.ToAsyncEnumerable());
    }

    public Task<IAsyncEnumerable<TResult>> GetAllAsync<TResult>(ISpecification<T> spec) where TResult : class
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(ISpecification<T> spec)
    {
        var entity = _store.Values.AsQueryable().FirstOrDefault(spec.Criteria);
        return await Task.FromResult(entity);
    }

    public Task<TResult> GetByIdAsync<TResult>(ISpecification<T> spec) where TResult : class, new()
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetFirstOrDefaultAsync(ISpecification<T> spec)
    {
        var entity = _store.Values.AsQueryable().FirstOrDefault(spec.Criteria);
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity of type {typeof(T)} not found.");
        }
        return await Task.FromResult(entity);
    }

    public Task<TResult> GetFirstOrDefaultAsync<TResult>(ISpecification<T> spec) where TResult : class
    {
        throw new NotImplementedException();
    }

    public Task<IAsyncEnumerable<T>> GetPagedListAsync(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }

    public Task<IAsyncEnumerable<TResult>> GetPagedListAsync<TResult>(ISpecification<T> spec) where TResult : class, new()
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        _store.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }

    public Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
            _store.TryAdd(entity.Id, entity);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
internal class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}