using System.Collections.Concurrent;

using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Contracts;

namespace CogniVault.Application.VirtualFileSystem.Provider.Memory;

public class MemoryDbResolver : IDbResolver
{
    private readonly ConcurrentDictionary<RepositoryContextKey, object> _repositories;

    public MemoryDbResolver()
    {
        _repositories = new ConcurrentDictionary<RepositoryContextKey, object>();
    }

    public IQueryRepositoryAsync<T> QueryRepository<T>() where T : FileSystemNode
    {
        var key = new RepositoryContextKey(typeof(T));
        if (!_repositories.TryGetValue(key, out var repo))
        {
            repo = new MemoryRepositoryAsync<T>();
            _repositories[key] = repo;
        }
        return (IQueryRepositoryAsync<T>)repo;
    }

    public ICommandRepositoryAsync<T> CommandRepository<T>() where T : FileSystemNode
    {
        return (ICommandRepositoryAsync<T>)QueryRepository<T>();
    }
}
