namespace CogniVault.Platform.Core.Entities;

public interface IEntityBase<TId> : IIdentityEntity<TId>
{
    bool IsTransient();
    bool Equals(object obj);
    int GetHashCode();
}