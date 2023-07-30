using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;

namespace CogniVault.Application.Abstractions.Resources;

public interface IFileSystemAddress
{
    // This property gets the address of the resource. The address is typically
    // a unique identifier that can be used to locate the resource within the
    // file system. The specific format of the address would depend on the
    // implementation, but it could be something like a file path or a URL.
    IFileSystemAddress Address { get; }

    // This property gets the file system that the resource is part of. This
    // could be used to perform operations on the file system, such as creating
    // or deleting files, or to get information about the file system, such as
    // its size or the amount of free space.
    IFileSystem FileSystem { get; }

    // This property gets the parent directory of the resource. This could be
    // used to navigate the file system hierarchy or to perform operations on
    // the parent directory, such as adding or removing files.
    IDirectory ParentDirectory { get; }
}