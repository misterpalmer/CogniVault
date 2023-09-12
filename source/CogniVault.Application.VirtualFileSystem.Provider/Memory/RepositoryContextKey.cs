namespace CogniVault.Application.VirtualFileSystem.Provider.Memory;

public class RepositoryContextKey
{
    public Type EntityType { get; }

    public RepositoryContextKey(Type entityType)
    {
        EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
    }

    public override bool Equals(object obj)
    {
        if (obj is RepositoryContextKey otherKey)
        {
            return EntityType.Equals(otherKey.EntityType);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return EntityType.GetHashCode();
    }
}

