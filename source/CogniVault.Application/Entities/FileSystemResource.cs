using CogniVault.Application.Abstractions;
using CogniVault.Application.Events;
using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class FileSystemResource : IResource, IEquatable<FileSystemResource>
{
    INamedResource INamedResource.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IUser Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsHidden { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IResourceAddress Address => throw new NotImplementedException();

    public IFileSystem FileSystem => throw new NotImplementedException();

    public IDirectory ParentDirectory => throw new NotImplementedException();

    public DateTimeOffset CreatedAt => throw new NotImplementedException();

    public DateTimeOffset LastModifiedAt => throw new NotImplementedException();

    public bool SupportsActivityEvents => throw new NotImplementedException();


    public event EventHandler<NameChangedEventArgs> Renamed;
    public event ResourceActivityEventHandler Created;
    public event ResourceActivityEventHandler Deleted;
    public event ResourceActivityEventHandler Changed;
    public event ResourceActivityEventHandler Activity;

    public int CompareTo(INamedResource? other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(FileSystemResource? other)
    {
        throw new NotImplementedException();
    }

    public INamedResource RenameTo(string name, bool overwrite)
    {
        throw new NotImplementedException();
    }

    public void SetOwner(IUser newOwner)
    {
        throw new NotImplementedException();
    }
}