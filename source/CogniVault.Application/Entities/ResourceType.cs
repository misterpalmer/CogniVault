namespace CogniVault.Application.Entities;

public abstract class ResourceType
{
    public Type Type { get; }

    protected ResourceType(Type type)
    {
        Type = type;
    }

    public abstract ResourceType FromName(string resourceTypeName);
}