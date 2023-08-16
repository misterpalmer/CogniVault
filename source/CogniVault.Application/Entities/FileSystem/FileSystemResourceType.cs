using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;
using CogniVault.Application.Abstractions.Resources.FileSystem.Files;
using CogniVault.Application.Abstractions.Resources.FileSystem.Symlink;

namespace CogniVault.Application.Entities;

public class FileSystemResourceType : ResourceType
{
    public static readonly FileSystemResourceType File = new FileSystemResourceType(typeof(IFile));
    public static readonly FileSystemResourceType Directory = new FileSystemResourceType(typeof(IDirectory));
    public static readonly FileSystemResourceType SymbolicLink = new FileSystemResourceType(typeof(ISymbolicLink));

    public FileSystemResourceType(Type type) : base(type) {}

    public override ResourceType FromName(string resourceTypeName) => resourceTypeName.ToUpper() switch
    {
        "FILE" or "F" => FileSystemResourceType.File,
        "DIRECTORY" or "D" => FileSystemResourceType.Directory,
        "SYM" or "S" => FileSystemResourceType.SymbolicLink,
        _ => throw new NotSupportedException($"FileSystemResourceType_{resourceTypeName}"),
    };
}