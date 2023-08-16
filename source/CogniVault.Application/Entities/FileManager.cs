using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Providers;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.Constants;

namespace CogniVault.Application.Entities;

public class FileManager : IFileManager
{
    private readonly IFileSystem _fileSystem;
    private readonly IAccessControlSecurityProvider _accessControl;

    public FileManager(IFileSystem fileSystem, IAccessControlSecurityProvider accessControl)
    {
        _fileSystem = fileSystem;
        _accessControl = accessControl;
    }

    public async Task<IFileSystemNode> CreateFileAsync(IFileSystemResource resource, IFileSystemNode parent, IFileSystemUser user)
    {
        bool hasAccess = await _accessControl.CheckPermissionAsync(user, resource, FileSystemSecuredOperation.Write);
        if (!hasAccess) throw new Exception("Access denied.");

        return await _fileSystem.FileSystemNodeFactory.CreateNodeAsync(resource, parent);
    }

    public async Task DeleteFileAsync(Guid nodeId, IFileSystemUser user)
    {
        IFileSystemNode nodeToDelete = await _fileSystem.GetNodeAsync(nodeId);

        bool hasAccess = await _accessControl.CheckPermissionAsync(user, nodeToDelete.Resource, FileSystemSecuredOperation.Delete);
        if (!hasAccess) throw new Exception("Access denied.");

        await _fileSystem.DeleteNodeAsync(nodeId);
    }

    public async Task MoveFileAsync(Guid nodeId, IFileSystemNode newParent, IFileSystemUser user)
    {
        IFileSystemNode nodeToMove = await _fileSystem.GetNodeAsync(nodeId);

        bool hasAccess = await _accessControl.CheckPermissionAsync(user, nodeToMove.Resource, FileSystemSecuredOperation.Write);
        if (!hasAccess) throw new Exception("Access denied.");

        await _fileSystem.MoveNodeAsync(nodeId, newParent);
    }

    public async Task<string> ReadResourceAsync(IFileSystemResource resource, IFileSystemUser user)
    {
        bool hasAccess = await _accessControl.CheckPermissionAsync(user, resource, FileSystemSecuredOperation.Read);
        if (!hasAccess) throw new Exception("Access denied.");

        return await _fileSystem.ReadAsync<string>(resource);
    }

    public async Task WriteResourceAsync(IFileSystemResource resource, string data, IFileSystemUser user)
    {
        bool hasAccess = await _accessControl.CheckPermissionAsync(user, resource, FileSystemSecuredOperation.Write);
        if (!hasAccess) throw new Exception("Access denied.");

        await _fileSystem.WriteAsync(resource, data);
    }

    public async Task LockResourceAsync(Guid resourceId)
    {
        var resource = new Resource(resourceId);
        await _fileSystem.LockResourceAsync(resource);
    }

    public async Task UnlockResourceAsync(Guid resourceId)
    {
        var resource = new Resource(resourceId);
        await _fileSystem.UnlockResourceAsync(resource);
    }
}


// public async Task<IEnumerable<IFileSystemNode>> FindByResourceNameAsync(string resourceName)
// {
//     return await _fileSystem.FindByResourceNameAsync(resourceName);
// }

// public async Task<IEnumerable<IFileSystemNode>> FindByUserAsync(IFileSystemUser user)
// {
//     return await _fileSystem.FindByUserAsync(user);
// }
