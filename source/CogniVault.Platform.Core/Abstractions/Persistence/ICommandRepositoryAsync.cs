using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Core.Abstractions.Persistence;

public interface ICommandRepositoryAsync<TEntity, TId> where TEntity : DomainEntityBase
{
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(object id, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task ExecuteSqlCommandAsync(string sql, params object[] parameters);
}
