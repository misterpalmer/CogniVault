using CogniVault.Application.VirtualFileSystem.Abstractions;
using CogniVault.Application.VirtualFileSystem.Entities;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IVirtualFileSystem
{
    RootNode Root { get; }
    // IFileSystemSecurityProvider FileSystemSecurityProvider { get; }
    // IAccessControlSecurityProvider AccessControlSecurityProvider { get; } 
    // IFileSystemNodeFactory FileSystemNodeFactory { get; }

    Task<RootNode> GetRootNodeAsync();
    Task<IEnumerable<FileSystemNode>> GetDirectoryAsync(Guid parent);
    Task<DirectoryNode> CreateDirectoryAsync(Guid parent, DirectoryName name);
    Task<FileSystemNode> GetNodeAsync(Guid id);
    Task DeleteNodeAsync(Guid id);
    Task MoveNodeAsync(Guid id, IFileSystemNode newParent);

    Task LockResourceAsync(IFileSystemNode resource);
    Task UnlockResourceAsync(IFileSystemNode resource);
    Task<T> ReadAsync<T>(IFileSystemNode resource);
    Task WriteAsync<T>(IFileSystemNode resource, T data);
}