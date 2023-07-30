using CogniVault.Application.Abstractions.Resources.FileSystem;

namespace CogniVault.Application.Abstractions.Operations.FileSystem;

public interface IFileSystemCreateOperation : IFileSystemOperation
{
    IFileSystemResource Create();
    IFileSystemResource Create(bool createParent);
}