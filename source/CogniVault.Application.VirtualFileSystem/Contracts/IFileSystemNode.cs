namespace CogniVault.Application.VirtualFileSystem.Contracts;

public interface IFileSystemNode
{
    /// <summary>
    /// The unique identifier of the node.
    /// This is used to identify the node in the filesystem.
    /// This is also used to identify the node in the database.
    /// </summary>
    Guid Id { get; set;}

    /// <summary>
    /// The parent node of the node.
    /// </summary>
    IFileSystemNode Parent { get; }

    // /// <summary>
    // /// The resource associated with the node.
    // /// </summary>
    // IFileSystemResource Resource { get; }

    // /// <summary>
    // /// The permissions associated with the node.
    // /// </summary>
    // IAccessControlPermissionNode Permissions { get; set; }

    // /// <summary>
    // /// The operations associated with the node.
    // /// </summary>
    // IFileSystemOperationNode Operations { get; set; }

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
}