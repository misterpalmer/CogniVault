using CogniVault.Application.Abstractions;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem.Directories;
using CogniVault.Application.Events;
using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class FileSystemResource : IResource, IEquatable<FileSystemResource>
{
    public ResourceType Type => throw new NotImplementedException();

    public Guid Id => throw new NotImplementedException();

    public IFileSystemUser Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsHidden { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IFileSystemUser CreatedBy => throw new NotImplementedException();

    public DateTimeOffset CreatedAt => throw new NotImplementedException();

    public IFileSystemUser LastModifiedBy => throw new NotImplementedException();

    public DateTimeOffset LastModifiedAt => throw new NotImplementedException();

    public bool SupportsActivityEvents => throw new NotImplementedException();

    public event ResourceActivityEventHandler Created;
    public event ResourceActivityEventHandler Deleted;
    public event ResourceActivityEventHandler Changed;
    public event ResourceActivityEventHandler Activity;

    public bool Equals(FileSystemResource? other)
    {
        throw new NotImplementedException();
    }

    public void SetOwner(IFileSystemUser newOwner)
    {
        throw new NotImplementedException();
    }
}