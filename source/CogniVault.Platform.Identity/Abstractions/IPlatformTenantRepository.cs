using CogniVault.Platform.Identity.Entities;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformTenantRepository
{
    /// <summary>
    /// Retrieves an organization from the repository using its name.
    /// </summary>
    /// <param name="name">The name of the organization to retrieve.</param>
    /// <returns>The organization if found; otherwise, null.</returns>
    Task<PlatformTenant?> GetByNameAsync(string name);
}