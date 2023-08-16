using CogniVault.Application.Abstractions.Operations.FileSystem;
using CogniVault.Application.Abstractions.Operations.FileSystem.Directories;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemOperations
    : IResourceAccess
    , IFileSystemMoveOperation
    , IFileSystemCopyOperation
    , IFileSystemCreateOperation
    , IFileSystemDeleteOperation
    , IFileSystemOperationTargetDirectory
{
}
