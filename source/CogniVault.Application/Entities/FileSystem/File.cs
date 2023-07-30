using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;
using CogniVault.Application.Abstractions.Resources.FileSystem.Files;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class File : FileSystemResource, IFile
{
    public IResourceContent Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public long Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IFileSystemNode Parent => throw new NotImplementedException();

    public ResourceName Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IFileSystemAddress Address => throw new NotImplementedException();

    public IFileSystem FileSystem => throw new NotImplementedException();

    public IDirectory ParentDirectory => throw new NotImplementedException();
}