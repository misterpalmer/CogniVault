using CogniVault.Application.Abstractions.Resources.FileSystem;

namespace CogniVault.Application.Abstractions.Operations.FileSystem;

public interface IFileSystemDeleteOperation : IFileSystemOperation
{
    IFileSystemResource Delete();
}