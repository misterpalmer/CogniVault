using CogniVault.Application.Abstractions.Providers;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;


namespace CogniVault.Application.Abstractions;


public interface IFileSystem
{
    IFileSystemNode Root { get; }
    IFileSystemSecurityProvider FileSystemSecurityProvider { get; }
    IAccessControlSecurityProvider AccessControlSecurityProvider { get; }
    IFileSystemNodeFactory FileSystemNodeFactory { get; }

    Task<IFileSystemNode> GetNodeAsync(Guid id);
    Task DeleteNodeAsync(Guid id);
    Task MoveNodeAsync(Guid id, IFileSystemNode newParent);

    Task LockResourceAsync(IResource resource);
    Task UnlockResourceAsync(IResource resource);
    Task<T> ReadAsync<T>(IResource resource);
    Task WriteAsync<T>(IResource resource, T data);

    Task<IEnumerable<IFileSystemNode>> FindByResourceNameAsync(string resourceName);
    Task<IEnumerable<IFileSystemNode>> FindByUserAsync(IFileSystemUser user);
}


// public interface IFileSystem : IResourceResolver, IDisposable
// {
//     bool IsDisposed { get; }
//     event EventHandler Closed;
//     IDirectory RootDirectory { get; }
//     FileSystemOptions Options { get; }
//     void Close();
// }