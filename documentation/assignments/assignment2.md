# Homework #2: Design Principles - Design Patterns

Refactor the code submitted in the previous assignment in light of the Design Patterns knowledge you have gained in today's class.

Rubric:

1. Refactor the code submitted incorporating the feedback (1 point)

2. Use of Encapsulation, Inheritance, and Polymorphism (2 points)

3. Incorporate use of Singleton pattern in your design. Think where you can use other design Design Patterns (2 points)*

*Remember: Not all Design Patterns may apply to your design. So think through how your design can be more flexible for future changes.*


## Submission

https://github.com/misterpalmer/CogniVault/commit/b144ea5b7c92e376c465ef0929cf01873997feed



###Feedback
1. I have refactored the code submitted in the previous assignment incorporating the feedback.

** I do not see relations ships during file operations between user's permissions and files.**

Permissions are set and retrieved from the resource manager. The resource manager is responsible for managing:
- the permissions of the resources
- operations that can be performed on the resources.

The resource manager is also responsible for managing the root directory of the file system.

```csharp
public interface IResourceManager
{
    IDirectory Root { get; }
    IResourceOperation DirectoryOperations { get; }
    IResourceOperation FileOperations { get; }
    void SetPermissions(IResource resource, IPermission permission);
    IPermission GetPermissions(IResource resource);
}
```
Permissions are set at the group and user level. The permissions are set on the resource. The resource is either a file or a directory. The permissions are set on the resource by the resource manager.

```csharp
public interface IPermission
{
    PermissionType Type { get; }
    IUser GrantedUser { get; }
    IGroup GrantedGroup { get; }
    // This method checks whether the permission allows the specified operation.
    bool Allows(FileSystemSecuredOperation operation);
}
```
`PermissionType` and `FileSystemSecuredOperation` represent similar concepts but different purposes.

`PermissionType` represents the different types of permissions that a user or a group can have on a resource. This includes permissions to read, write, and delete a resource, but it also include more specific or more complex permissions. For example, a `PermissionType` could represent the permission to append data to a resource, the permission to execute a resource, or the permission to change the permissions of a resource.

`FileSystemSecuredOperation` represent different types of operations that can be performed on a file system resource, such as reading, writing, deleting, moving, copying, and renaming a resource. This enumeration is used to specify the operation that a method is trying to perform, or to specify the operation that a permission needs to allow.

In some cases, the values of `PermissionType` and `FileSystemSecuredOperation` might correspond one-to-one. For example,:
- a Read operation might require a Read permission,
- a Write operation might require a Write permission,

and so on. But in other cases,
- an operation might require multiple permissions,
- or a permission might allow multiple operations.

For example, a Move operation might require both Read and Delete permissions, and a FullControl permission might allow all operations.

So, while `FileSystemSecuredOperation` and `PermissionType` have similar values, they represent different concepts with different purposes.

```csharp
public enum PermissionType
{
    Read,
    Write,
    Execute
}

public enum FileSystemSecuredOperation
{
    // Represents a read operation on a resource.
    Read,

    // Represents a write operation on a resource.
    Write,

    // Represents a delete operation on a resource.
    Delete,

    // Represents an operation to change the permissions of a resource.
    ChangePermissions,

    // Represents an operation to move a resource.
    Move,

    // Represents an operation to copy a resource.
    Copy,

    // Represents an operation to rename a resource.
    Rename
}
```


2. I have used Encapsulation, Inheritance, and Polymorphism in my design.

```csharp
public interface IFileSystem : IResourceResolver, IDisposable
{
    bool IsDisposed { get; }
    event EventHandler Closed;
    IDirectory RootDirectory { get; }
    IService GetService(IResource node);
    ResourceType GetResourceType(string path);
    bool ResourceExists(string path, ResourceType nodeType);
    FileSystemOptions Options { get; }
    IResource Resolve(IResourceAddress address, ResourceType nodeType);
    IResourceManager SecurityManager { get; }
    bool HasAccess(IResource node, FileSystemSecuredOperation operation);
    void CheckAccess(IResource node, FileSystemSecuredOperation operation);
    void Close();
}
```

```csharp
namespace CogniVault.FileSystem.Provider.Abstractions;

public class AbstractNodeProvider : IFileSystemProvider
{
    
}

namespace CogniVault.FileSystem.Provider.Memory;

public class MemoryFileSystemProvider : IFileSystemProvider
{
    private readonly ConcurrentDictionary<Guid, IFileSystem> _fileSystems = new();

    public Task<IFileSystem> GetFileSystemAsync(Guid id)
    {
        return Task.FromResult(_fileSystems[id]);
    }

    public Task<IFileSystem> CreateFileSystemAsync()
    {
        var fileSystem = new MemoryFileSystem();
        _fileSystems.TryAdd(fileSystem.Id, fileSystem);
        return Task.FromResult<IFileSystem>(fileSystem);
    }
}
```

3. I have incorporated the use of the Singleton pattern.

```csharp
public class FileSystem : IFileSystem
{
    // Static instance of the class, instantiated once
    private static readonly FileSystem instance = new FileSystem();

    // Private constructor to prevent instantiation
    private FileSystem()
    {
        // Initialization code here
    }

    private bool _disposedValue;

    public bool IsDisposed => throw new NotImplementedException();

    public IDirectory RootDirectory => throw new NotImplementedException();

    public FileSystemOptions Options => throw new NotImplementedException();

    public IResourceManager SecurityManager => throw new NotImplementedException();

    public event EventHandler Closed;

    public void CheckAccess(IResource node, FileSystemSecuredOperation operation)
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        throw new NotImplementedException();
    }

    public ResourceType GetResourceType(string path)
    {
        throw new NotImplementedException();
    }

    public IService GetService(IResource node)
    {
        throw new NotImplementedException();
    }

    public bool HasAccess(IResource node, FileSystemSecuredOperation operation)
    {
        throw new NotImplementedException();
    }

    public IResource Resolve(IResourceAddress address, ResourceType nodeType)
    {
        throw new NotImplementedException();
    }

    public bool ResourceExists(string path, ResourceType nodeType)
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~FileSystem()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
```