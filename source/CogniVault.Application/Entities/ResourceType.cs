using CogniVault.Application.Abstractions;

namespace CogniVault.Application.Entities;

public class ResourceType
{
    public static readonly ResourceType File = new ResourceType(typeof(IFile));
    public static readonly ResourceType Directory = new ResourceType(typeof(IDirectory));
    public static readonly ResourceType SymbolicLink = new ResourceType(typeof(ISymbolicLink));

    public ResourceType(Type type)
    {
        Type = type;
    }

    public Type Type { get; }

    public static ResourceType FromName(string resourceTypeName) => resourceTypeName.ToUpper() switch
    {
        "FILE" or "F" => ResourceType.File,
        "DIRECTORY" or "D" => ResourceType.Directory,
        "SYM" or "S" => ResourceType.SymbolicLink,
        _ => throw new NotSupportedException($"ResourceType_{resourceTypeName}"),
    };
}