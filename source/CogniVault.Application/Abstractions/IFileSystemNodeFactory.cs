using CogniVault.Application.Abstractions.Resources.FileSystem;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemNodeFactory
{
    Task<IFileSystemNode> CreateRootNodeAsync();
    Task<IFileSystemNode> CreateNodeAsync(IFileSystemResource resource, IFileSystemNode parent);
}