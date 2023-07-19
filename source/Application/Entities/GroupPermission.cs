using CogniVault.Application.Constants;
using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Entities;

public class GroupPermission : IPermission
{
    public PermissionType Type { get; private set; }
    public IUser? GrantedUser { get; private set; } // Null for GroupPermission
    public IGroup GrantedGroup { get; private set; }

    public GroupPermission(PermissionType type, IGroup grantedGroup)
    {
        Type = type;
        GrantedGroup = grantedGroup;
    }
}