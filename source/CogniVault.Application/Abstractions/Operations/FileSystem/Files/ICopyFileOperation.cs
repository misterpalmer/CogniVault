using CogniVault.Application.Abstractions.Operations.FileSystem.Directories;
using CogniVault.Application.Abstractions.Resources.FileSystem;

namespace CogniVault.Application.Abstractions.Operations.FileSystem.Files;
public interface IFileSystemCopyOperation : IFileSystemOperation
{
    IFileSystemResource CopyTo(IFileSystemResource target, bool overwrite);
    IFileSystemResource CopyToDirectory(IFileSystemOperationTargetDirectory target, bool overwrite);
}