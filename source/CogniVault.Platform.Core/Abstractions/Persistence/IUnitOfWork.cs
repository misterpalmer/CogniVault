using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Core.Abstractions.Persistence;

public interface IUnitOfWork : IDisposable
{
    IQueryRepositoryAsync<TEntity, TId> QueryRepository<TEntity, TId>() where TEntity : DomainEntityBase;
    ICommandRepositoryAsync<TEntity, TId> CommandRepository<TEntity, TId>() where TEntity : DomainEntityBase;
    Task<int> CompleteAsync();
}

