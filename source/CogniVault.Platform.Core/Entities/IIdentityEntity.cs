namespace CogniVault.Platform.Core.Entities;

public interface IIdentityEntity<T>
{
    /// <summary>
    ///     Gets or sets the integer identifier of the entity.
    /// </summary>
    public T Id { get; set; }
}