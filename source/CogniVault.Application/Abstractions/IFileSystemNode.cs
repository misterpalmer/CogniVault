using CogniVault.Application.Abstractions.Operations.FileSystem;
using CogniVault.Application.Abstractions.Permissions;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemNode
{
    Guid Id { get; }
    IFileSystemResource Resource { get; }
    IFileSystemNode Parent { get; }
    List<IFileSystemNode> Children { get; }

    IAccessControlPermissionNode Permissions { get; set; }
    IFileSystemOperationNode Operations { get; set; }

    Task LockAsync();
    Task UnlockAsync();
    
    Task<bool> IsLockedAsync();
    Task<T> ReadAsync<T>();
    Task WriteAsync<T>(T data);

    Task<IFileSystemNode> FindChildByResourceNameAsync(ResourceName resourceName);
    Task<IFileSystemNode> FindChildByUserAsync(IFileSystemUser user);
}


// public interface IFileSystemNode
// {
//     Guid Id { get; set; }
//     IFileSystemResource Resource { get; set; }
//     IPermissionNode Permissions { get; set; }
//     IOperationNode Operations { get; set; }
//     IFileSystemNode Parent { get; set; }
//     List<IFileSystemNode> Children { get; set; }
// }
