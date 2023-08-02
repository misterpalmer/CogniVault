namespace CogniVault.Application.Abstractions;

public interface IFileManager
{
    Task<bool> MoveNodeAsync(Guid nodeId, Guid newParentId);
    Task DeleteNodeAsync(Guid nodeId);
    Task<T> ReadResourceAsync<T>(Guid resourceId);
    Task WriteResourceAsync<T>(Guid resourceId, T data);
    Task LockResourceAsync(Guid resourceId);
    Task UnlockResourceAsync(Guid resourceId);
}

// Task<IEnumerable<IFileSystemNode>> SearchAsync(ISearchCriteria criteria):
// This method would take an interface ISearchCriteria as a parameter, which could be implemented to provide different types of search (by user, by resource name, by date, etc.)

// Task<IEnumerable<IFileSystemNode>> TraverseAsync(ITraversalStrategy strategy):
// This method would take a traversal strategy as a parameter, which could be implemented for different traversal algorithms (depth-first, breadth-first, etc.)

// Task<IFileSystemNode> FindNodeAsync(Func<IFileSystemNode, bool> predicate):
// This method would take a predicate function and return the first node that matches the predicate.

// Task<IEnumerable<IFileSystemNode>> FindNodesAsync(Func<IFileSystemNode, bool> predicate):
// This method would take a predicate function and return all nodes that match the predicate.

// Task<IEnumerable<IFileSystemNode>> GetNodesAsync(Func<IFileSystemNode, IEnumerable<IFileSystemNode>> selector):
// This method would take a function that, given a node, returns a sequence of nodes. This could be used for various types of queries, such as getting all ancestors of a node, all descendants, all siblings, etc.



// Task<IEnumerable<IFileSystemNode>> GetChildrenAsync(Guid nodeId): This method would take a node Id and return all child nodes of the node. This would be useful for a hierarchical traversal of the tree.

// Task<IFileSystemNode> GetParentAsync(Guid nodeId): This method would return the parent node of a given node Id. This could be useful when you want to traverse up the tree.

// Task<IEnumerable<IFileSystemNode>> GetSiblingsAsync(Guid nodeId): This method would return all sibling nodes of a given node Id. This could be useful when you want to perform operations at the same hierarchical level.

// Task<IEnumerable<IFileSystemNode>> GetNodesByPermissionAsync(PermissionType permissionType): This method would take a PermissionType and return all nodes where the current user has the specified permission.

// Task<IEnumerable<IFileSystemNode>> GetLockedNodesAsync(): This method would return all nodes that are currently locked. This could be useful for monitoring and managing concurrent access to the filesystem.

