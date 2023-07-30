using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;

namespace CogniVault.Application.Abstractions.Operations.FileSystem.Directories;

public interface IFileSystemOperationTargetDirectory
{
    IDirectory OperationTargetDirectory { get; }
}