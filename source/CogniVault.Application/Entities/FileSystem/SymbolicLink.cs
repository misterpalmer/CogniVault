using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.Abstractions.Resources.FileSystem.Symlink;

namespace CogniVault.Application.Entities.FileSystem;

public class SymbolicLink : FileSystemResource, ISymbolicLink
{
    public IResource Target => throw new NotImplementedException();

    public ResourceType Type => throw new NotImplementedException();

    public Guid Id => throw new NotImplementedException();

    public IFileSystemUser CreatedBy => throw new NotImplementedException();

    public IFileSystemUser LastModifiedBy => throw new NotImplementedException();

    IFileSystemUser IOwnableResource.Owner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void SetOwner(IFileSystemUser newOwner)
    {
        throw new NotImplementedException();
    }

    public void SetTarget(IResource target)
    {
        throw new NotImplementedException();
    }
}