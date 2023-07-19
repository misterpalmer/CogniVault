using CogniVault.Application.Constants;
using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Interfaces;

public interface IPermission
{
    PermissionType Type { get; }
    IUser GrantedUser { get; }
    IGroup GrantedGroup { get; }
}
