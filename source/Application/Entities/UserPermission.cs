using CogniVault.Application.Constants;
using CogniVault.Application.Interfaces;


namespace CogniVault.Application.Entities;

public class UserPermission : IPermission
{
    public PermissionType Type { get; private set; }
    public IUser GrantedUser { get; private set; }
    public IGroup? GrantedGroup { get; private set; } // Null for UserPermission

    public UserPermission(PermissionType type, IUser grantedUser)
    {
        Type = type;
        GrantedUser = grantedUser;
    }
}