using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IIdentityRepository<TEntity, TId> where TEntity : DomainEntityBase
{
    /// <summary>
    /// Adds an entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity from the repository using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    Task DeleteAsync(TId id);

    /// <summary>
    /// Retrieves an entity from the repository using its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A collection of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();
}