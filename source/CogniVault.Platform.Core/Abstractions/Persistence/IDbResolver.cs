namespace CogniVault.Platform.Core.Abstractions.Persistence;

public interface IDbResolver
{
    /// <summary>
    ///     This method resolves the context (application-specific) and returns it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    TEntity GetContext<TEntity>() where TEntity : class;
}