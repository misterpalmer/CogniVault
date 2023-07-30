using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl;

public interface IAccessControlResource : IResource
{
    ResourceName Name { get; set; }
    // ... other common properties and methods
}

// public interface IUser : IAccessControlResource
// {
//     // ... user-specific properties and methods
// }

// public interface IGroup : IAccessControlResource
// {
//     // ... group-specific properties and methods
// }

// public interface IFileSystemResource : IAccessControlResource
// {
//     // ... common properties and methods for file system resources
// }

// public interface IFile : IFileSystemResource
// {
//     // ... file-specific properties and methods
// }

// public interface IDirectory : IFileSystemResource
// {
//     // ... directory-specific properties and methods
// }

// public interface ISymlink : IFileSystemResource
// {
//     // ... symlink-specific properties and methods
// }
