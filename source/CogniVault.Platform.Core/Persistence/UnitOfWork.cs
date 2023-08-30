using CogniVault.Platform.Core.Abstractions.Persistence;

namespace CogniVault.Platform.Core.Persistence;

// public class UnitOfWork : IUnitOfWork
// {
//     private readonly IDictionary<Type, object> _repositories = new Dictionary<Type, object>();
//     private readonly IDbContext _context;  // abstracted interface

//     public UnitOfWork(IDbContext context)
//     {
//         _context = context;
//     }

//     public IQueryRepositoryAsync<T> QueryRepository<T>() where T : class
//     {
//         if (_repositories.Keys.Contains(typeof(T)))
//             return _repositories[typeof(T)] as IQueryRepositoryAsync<T>;

//         var repository = new GenericQueryRepository<T>(_context);
//         _repositories.Add(typeof(T), repository);

//         return repository;
//     }

//     public ICommandRepositoryAsync<T> CommandRepository<T>() where T : class
//     {
//         if (_repositories.Keys.Contains(typeof(T)))
//             return _repositories[typeof(T)] as ICommandRepositoryAsync<T>;

//         var repository = new GenericCommandRepository<T>(_context);
//         _repositories.Add(typeof(T), repository);

//         return repository;
//     }

//     public async Task<int> CompleteAsync()
//     {
//         return await _context.SaveChangesAsync();  // using abstracted method
//     }

//     public void Dispose()
//     {
//         _context.Dispose();
//     }
// }

