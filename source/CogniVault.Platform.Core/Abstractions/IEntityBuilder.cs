using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Core.Abstractions;

public interface IEntityBuilder<TEntity>
{
    Task<TEntity> BuildAsync();
}