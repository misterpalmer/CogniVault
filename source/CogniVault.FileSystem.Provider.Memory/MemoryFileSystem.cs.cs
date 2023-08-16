using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Providers;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;

namespace CogniVault.FileSystem.Provider.Memory;
public class MemoryFileSystem : IFileSystem
{
    public IFileSystemNode Root => throw new NotImplementedException();

    public IFileSystemSecurityProvider FileSystemSecurityProvider => throw new NotImplementedException();

    public IAccessControlSecurityProvider AccessControlSecurityProvider => throw new NotImplementedException();

    public IFileSystemNodeFactory FileSystemNodeFactory => throw new NotImplementedException();

    public Task DeleteNodeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IFileSystemNode>> FindByResourceNameAsync(string resourceName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<IFileSystemNode>> FindByUserAsync(IFileSystemUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IFileSystemNode> GetNodeAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task LockResourceAsync(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task MoveNodeAsync(Guid id, IFileSystemNode newParent)
    {
        throw new NotImplementedException();
    }

    public Task<T> ReadAsync<T>(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task UnlockResourceAsync(IResource resource)
    {
        throw new NotImplementedException();
    }

    public Task WriteAsync<T>(IResource resource, T data)
    {
        throw new NotImplementedException();
    }
}
