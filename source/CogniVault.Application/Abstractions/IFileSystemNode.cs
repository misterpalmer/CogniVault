using CogniVault.Application.Abstractions.Operations.FileSystem;
using CogniVault.Application.Abstractions.Permissions;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemNode
{
    /// <summary>
    /// The unique identifier of the node.
    /// This is used to identify the node in the filesystem.
    /// This is also used to identify the node in the database.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The resource associated with the node.
    /// </summary>
    IFileSystemResource Resource { get; }

    /// <summary>
    /// The parent node of the node.
    /// </summary>
    IFileSystemNode Parent { get; }

    /// <summary>
    /// The children nodes of the node.
    /// </summary>
    List<IFileSystemNode> Children { get; }

    /// <summary>
    /// The permissions associated with the node.
    /// </summary>
    IAccessControlPermissionNode Permissions { get; set; }

    /// <summary>
    /// The operations associated with the node.
    /// </summary>
    IFileSystemOperationNode Operations { get; set; }

    /// <summary>
    /// The lock associated with the node.
    /// </summary>
    Task LockAsync();

    /// <summary>
    /// The unlock associated with the node.
    /// </summary>
    Task UnlockAsync();
    
    /// <summary>
    /// The check if the node is locked.
    /// </summary>
    Task<bool> IsLockedAsync();
    
    /// <summary>
    /// The read operation associated with the node.
    /// </summary>
    Task<T> ReadAsync<T>();

    /// <summary>
    /// The write operation associated with the node.
    /// </summary>
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
