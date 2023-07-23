using CogniVault.Application.Constants;
using CogniVault.Application.Interfaces;

public enum PermissionType
{
    Read,
    Write,
    Delete,
    // Add more permission types here as needed...
}

public class Permission : IPermission
{
    public PermissionType Type { get; }
    public IUser User { get; }
    public IGroup Group { get; }

    public IUser GrantedUser { get;}

    public IGroup GrantedGroup { get;}

    public Permission(PermissionType type, IUser user)
    {
        Type = type;
        GrantedUser = user ?? throw new ArgumentNullException(nameof(user));
    }

    public Permission(PermissionType type, IGroup group)
    {
        Type = type;
        GrantedGroup = group ?? throw new ArgumentNullException(nameof(group));
    }

    public bool Allows(FileSystemSecuredOperation operation)
    {
        // Implement this method to check whether the permission allows the specified operation.
        // This could be based on the Type property and the operation parameter.
        throw new NotImplementedException();
    }
}

