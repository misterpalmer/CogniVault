using CogniVault.Application.Abstractions.Permissions;
using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Groups;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Constants;

public class Permission : IAccessControlPermission
{
    public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public PermissionType PermissionType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileSystemUser User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileSystemGroup Group { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IResource Resource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime GrantedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileSystemUser GrantedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool Allows(FileSystemSecuredOperation operation)
    {
        throw new NotImplementedException();
    }
}


// public PermissionType Type { get; }
// public IFileSystemUser User { get; }
// public IFileSystemGroup Group { get; }

// public IFileSystemUser GrantedUser { get;}

// public IFileSystemGroup GrantedGroup { get;}

// public Permission(PermissionType type, IFileSystemGroup group)
// {
//     Type = type;
//     GrantedGroup = group ?? throw new ArgumentNullException(nameof(group));
// }

// public bool Allows(FileSystemSecuredOperation operation)
// {
//     // Implement this method to check whether the permission allows the specified operation.
//     // This could be based on the Type property and the operation parameter.
//     throw new NotImplementedException();
// }