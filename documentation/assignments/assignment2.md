# Homework #2: Design Principles - Design Patterns

Refactor the code submitted in the previous assignment in light of the Design Patterns knowledge you have gained in today's class.

Rubric:

1. Refactor the code submitted incorporating the feedback (1 point)

2. Use of Encapsulation, Inheritance, and Polymorphism (2 points)

3. Incorporate use of Singleton pattern in your design. Think where you can use other design Design Patterns (2 points)*

*Remember: Not all Design Patterns may apply to your design. So think through how your design can be more flexible for future changes.*


## Submission

https://github.com/misterpalmer/CogniVault/tree/b144ea5b7c92e376c465ef0929cf01873997feed

```cmd
$ git log b144ea5b7c92e376c465ef0929cf01873997
commit b144ea5b7c92e376c465ef0929cf01873997feed (origin/feature/assignment2, feature/assignment2)
Author: Matthew Andrew <matthew.andrew@grundens.com>
Date:   Sat Jul 22 23:57:50 2023 -0700

    feat:assignment2
```

All pertininent code is in the `source` directory.
https://github.com/misterpalmer/CogniVault/tree/b144ea5b7c92e376c465ef0929cf01873997feed/source


### Feedback
#### I have refactored the code submitted in the previous assignment incorporating the feedback.

[X] I do not see relationships during file operations between user's permissions and files.
Permissions are set and retrieved from the resource manager. The resource manager is responsible for managing:
- the permissions of the resources,
- operations that can be performed on the resources,
- also, responsible for managing the root directory of the file system.

```csharp
public interface IResourceManager
{
    IDirectory Root { get; }
    IResourceOperation DirectoryOperations { get; }
    IResourceOperation FileOperations { get; }
    void SetPermissions(IResource resource, IPermission permission);
    IPermission GetPermissions(IResource resource);
}

public interface IResourceOperations :
    IResourceAccess, IResourceMoveOperation
    , IResourceCopyOperation, IResourceCreateOperation
    , IResourceDeleteOperation, IResourceTargetDirectory
{
    // code removed for brevity
}
```

[-] I do not see permissions in Users or Files/Directories to connect them together.

Permissions are set on the resource (group and user level). The permissions are set on the resource by the resource manager. In addition, each resource has a special user--owner of the resource. This user has rights to all permissions and operations. The owner of a resource can be changed by calling the `SetOwner` method on the resource.

```csharp
public interface IPermission
{
    PermissionType Type { get; }
    IUser GrantedUser { get; }
    IGroup GrantedGroup { get; }
    // This method checks whether the permission allows the specified operation.
    bool Allows(FileSystemSecuredOperation operation);
}

public interface IResource : INamedResource, IResourceAddress, IResourceProperties, IResourceActivity
{
    IUser Owner { get; set; }
    void SetOwner(IUser newOwner);
    bool IsHidden { get; set; }
}
```

[x] I do not see permissions during file/directory operations.
`PermissionType` and `FileSystemSecuredOperation` represent similar concepts but different purposes.

`PermissionType` represents the different types of permissions that a user or a group can have on a resource. This includes permissions to read, write, and delete a resource, but it also include more specific or more complex permissions. For example, a `PermissionType` could represent the permission to append data to a resource, the permission to execute a resource, or the permission to change the permissions of a resource.

`FileSystemSecuredOperation` represent different types of operations that can be performed on a file system resource, such as reading, writing, deleting, moving, copying, and renaming a resource. This enumeration is used to specify the operation that a method is trying to perform, or to specify the operation that a permission needs to allow.

```csharp
public enum PermissionType
{
    Read,
    Write,
    Execute
}

public enum FileSystemSecuredOperation
{
    Read, // Represents a read operation on a resource.
    Write, // Represents a write operation on a resource.
    Delete, // Represents a delete operation on a resource.
    ChangePermissions, // Represents an operation to change the permissions of a resource.
    Move, // Represents an operation to move a resource.
    Copy, // Represents an operation to copy a resource.
    Rename // Represents an operation to rename a resource.
}
```

In some cases, the values of `PermissionType` and `FileSystemSecuredOperation` might correspond one-to-one. For example,:
- a Read operation might require a Read permission,
- a Write operation might require a Write permission,

and so on. But in other cases,
- an operation might require multiple permissions,
- or, a permission might allow multiple operations.

For example, a Move operation might require both Read and Delete permissions, and a FullControl permission might allow all operations.

So, while `FileSystemSecuredOperation` and `PermissionType` have similar values, they represent different concepts with different purposes.

2. I have used Encapsulation, Inheritance, and Polymorphism in my design.

Encapsulation is the bundling of data and the methods that operate on that data into a single unit, a class. Eencapsulation is used in the `MemoryFileSystemProvider` class. The class encapsulates the data `_fileSystems` and the methods that operate on this data like `GetFileSystemAsync` and `CreateFileSystemAsync`. This way, the internal state of the `_fileSystems` is hidden from the outside world, and can only be accessed and modified through the provided methods

```csharp
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

Inheritance is a mechanism where a class is a derivitave from another class for a hierarchy of classes that share a set of attributes and methods. `AbstractNodeProvider` and `MemoryFileSystemProvider` both implement the `IFileSystemProvider` interface. This means that they both inherit the interface's contract and must provide implementations for its methods. Inheritance permits use of an instance from any of these classes wherever an IFileSystemProvider is expected.

```csharp
public class AbstractNodeProvider : IFileSystemProvider
{
    // Implementation of IFileSystemProvider methods
}

public class MemoryFileSystemProvider : IFileSystemProvider
{
    // Implementation of IFileSystemProvider methods
    // code removed for brevity
    ...
}
```

Polymorphism is the ability of an object to take on many forms. The most common use of polymorphism in OOP occurs when a parent class reference is used to refer to a child class object. Polymorphism is used with the `IFileSystemProvider` interface. An instance of `MemoryFileSystemProvider` is created to use as an `IFileSystemProvider`. This allows the actual implementation of the file system provider to change without changing the code that uses the `IFileSystemProvider`.

```csharp
IFileSystemProvider provider = new MemoryFileSystemProvider();
// Use provider as an IFileSystemProvider
```

`IFileSystem` interface encapsulates properties and methods related to a file system, hiding the internal details. any class implementing the `IFileSystem` interface, like `MyFileSystem`, inherits its contract and must provide implementations for its methods. `IFileSystem`, such as `MyFileSystem` or `DiskFileSystem`, can be used as an `IFileSystem`, demonstrating polymorphism.


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

3. I have incorporated the use of the Singleton pattern.

The `FileSystem` class is an implementation of the `IFileSystem` interface. It is designed as a singleton class, which means that only one instance of this class can exist in the application. This is done to provide a global point of access to the file system without creating multiple instances.

Here's a breakdown of the class:

- `private static readonly FileSystem instance`: This line creates a single, static, read-only instance of the `FileSystem` class. This instance is created when the class is loaded, and it is the only instance of the `FileSystem` class that will ever be created.
- `private FileSystem()`: This is the constructor of the `FileSystem` class. It is marked as private to prevent other classes from creating new instances of the `FileSystem` class.
- `public IDirectory RootDirectory`: This is a property that represents the root directory of the file system. 
- `public FileSystemOptions Options`: This is a property that represents the options for the file system.
- `public IResourceManager SecurityManager`: This is a property that represents the security manager for the file system.


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

    public IDirectory RootDirectory => throw new NotImplementedException();

    public FileSystemOptions Options => throw new NotImplementedException();

    public IResourceManager SecurityManager => throw new NotImplementedException();

    // code removed for brevity
    ...
}
```
The **Singleton Design Pattern** is a type of creational pattern which ensures that a class has only one instance and provides a global point of access to it. This is useful when exactly one object is needed to coordinate actions across the system, such as a file system or a database connection.

In the `FileSystem` class, the singleton pattern is implemented by:

1. **Static Readonly Instance:** A static readonly field `instance` is declared that holds the single instance of the `FileSystem` class. The `static` keyword is used to declare members that belong to the type itself rather than to instances of the type. The `readonly` keyword ensures that the instance cannot be changed after it's initialized.

```csharp
private static readonly FileSystem instance = new FileSystem();
```

2. **Private Constructor:** The constructor of the `FileSystem` class is marked as private to prevent other classes from creating new instances of the `FileSystem` class.

```csharp
private FileSystem()
{
    // Initialization code here
}
```
3. **Public Static Instance Getter:** A public static property `Instance` is declared that returns the single instance of the `FileSystem` class. This property is static, which means that it can be accessed without creating an instance of the `FileSystem` class. This property is also read-only, which means that it cannot be changed after it's initialized.

```csharp
public static FileSystem Instance => instance;
```

This way, all parts of the application that need to use the FileSystem class will use the same instance, ensuring consistent behavior across the system.


```csharp
// Accessing the singleton instance
var fileSystem = FileSystem.Instance;

// Define a path
string path = "/path/to/resource";

// Get the resource type
var resourceType = fileSystem.GetResourceType(path);
Console.WriteLine($"Resource type of {path}: {resourceType}");

// Check if the resource exists
bool exists = fileSystem.ResourceExists(path, resourceType);
Console.WriteLine($"Does the resource exist? {exists}");

// Dispose the file system when done
fileSystem.Dispose();
```