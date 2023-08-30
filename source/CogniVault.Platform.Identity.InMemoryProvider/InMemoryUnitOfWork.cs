using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Repositories;

namespace CogniVault.Platform.Identity.InMemoryProvider;

public class InMemoryUnitOfWork : IUnitOfWork
{
    private struct RepositoryKey
    {
        public Type EntityType { get; }
        public Type IdType { get; }

        public RepositoryKey(Type entityType, Type idType)
        {
            EntityType = entityType;
            IdType = idType;
        }

        public override bool Equals(object obj)
        {
            if (obj is RepositoryKey key)
            {
                return EntityType == key.EntityType && IdType == key.IdType;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EntityType, IdType);
        }
    }

    private readonly Dictionary<RepositoryKey, object> _repositories = new Dictionary<RepositoryKey, object>();
    private bool _disposed;

    public IQueryRepositoryAsync<TEntity, TId> QueryRepository<TEntity, TId>() 
        where TEntity : DomainEntityBase
    {
        var key = new RepositoryKey(typeof(TEntity), typeof(TId));

        if (_repositories.TryGetValue(key, out var repo))
        {
            return (IQueryRepositoryAsync<TEntity, TId>)repo;
        }

        var repositoryType = typeof(InMemoryRepositoryAsync<,>).MakeGenericType(typeof(TEntity), typeof(TId));
        var repositoryInstance = Activator.CreateInstance(repositoryType);

        _repositories[key] = repositoryInstance;
        return (IQueryRepositoryAsync<TEntity, TId>)repositoryInstance;
    }

    public ICommandRepositoryAsync<TEntity, TId> CommandRepository<TEntity, TId>() 
        where TEntity : DomainEntityBase
    {
        return (ICommandRepositoryAsync<TEntity, TId>) QueryRepository<TEntity, TId>();
    }

    public Task<int> CompleteAsync()
    {
        return Task.FromResult(1); // No actual commit as this is in-memory.
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repositories.Clear();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}


