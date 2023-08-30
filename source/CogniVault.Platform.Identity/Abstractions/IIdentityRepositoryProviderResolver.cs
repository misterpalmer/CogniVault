using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IIdentityRepositoryResolver
{
    /// <summary>
    /// Resolves the appropriate repository for a given entity type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TId">The type of the entity's identifier.</typeparam>
    /// <returns>The resolved repository instance.</returns>
    IIdentityRepository<T, TId> Resolve<T, TId>() where T : DomainEntityBase;
}