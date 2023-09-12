namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface ICommandRepositoryAsync<T> where T : class
{
    Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);

    Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(object id, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}