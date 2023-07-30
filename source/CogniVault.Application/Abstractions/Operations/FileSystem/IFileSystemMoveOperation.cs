using CogniVault.Application.Abstractions.Resources.FileSystem;

namespace CogniVault.Application.Abstractions.Operations.FileSystem;

public interface IFileSystemMoveOperation : IFileSystemOperation
{
    IFileSystemResource MoveTo(IFileSystemResource target, bool overwrite);
    IFileSystemResource MoveToDirectory(IFileSystemResource target, bool overwrite);
}