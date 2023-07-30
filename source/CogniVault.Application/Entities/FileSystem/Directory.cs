using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class Directory : FileSystemResource, IDirectory
{
    public IFileSystemNode Parent => throw new NotImplementedException();

    public ResourceName Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IFileSystemAddress Address => throw new NotImplementedException();

    public IFileSystem FileSystem => throw new NotImplementedException();

    public IDirectory ParentDirectory => throw new NotImplementedException();
}
