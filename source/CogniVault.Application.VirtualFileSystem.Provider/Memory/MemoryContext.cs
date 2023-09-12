using System.Collections.Concurrent;
using CogniVault.Application.VirtualFileSystem.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CogniVault.Application.VirtualFileSystem.Provider.Memory;

// public class MemoryContext : IUnitOfWork
// {
//     private readonly IServiceProvider _serviceProvider;
    
//     private readonly ConcurrentDictionary<RepositoryContextKey, object> _repositories = new ConcurrentDictionary<RepositoryContextKey, object>();
    
//     private bool _disposed;

//     public MemoryContext(IServiceProvider serviceProvider)
//     {
//         _serviceProvider = serviceProvider;
//     }

//     public IQueryRepositoryAsync<T> QueryRepository<T>() where T : class
//     {
//         var key = new RepositoryContextKey(typeof(T));

//         if (_repositories.TryGetValue(key, out var repo))
//         {
//             return (IQueryRepositoryAsync<T>)repo;
//         }

//         // var repositoryType = typeof(MemoryRepositoryAsync<>).MakeGenericType(typeof(T));
//         var repositoryType = typeof(IQueryRepositoryAsync<>).MakeGenericType(typeof(T));
//         var repositoryInstance = _serviceProvider.GetRequiredService(repositoryType);

//         _repositories[key] = repositoryInstance;
//         return (IQueryRepositoryAsync<T>)repositoryInstance;
//     }

//     public ICommandRepositoryAsync<T> CommandRepository<T>() where T : class
//     {
//         return (ICommandRepositoryAsync<T>)QueryRepository<T>();
//     }

//     public Task<int> CompleteAsync()
//     {
//         return Task.FromResult(1);
//     }

//     protected virtual void Dispose(bool disposing)
//     {
//         if (!_disposed)
//         {
//             if (disposing)
//             {
//                 _repositories.Clear();
//             }
//         }
//         _disposed = true;
//     }

//     public void Dispose()
//     {
//         Dispose(true);
//         GC.SuppressFinalize(this);
//     }
// }