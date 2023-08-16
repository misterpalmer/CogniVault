using CogniVault.Application.Abstractions.Permissions;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.Constants;

namespace CogniVault.Application.Abstractions.Providers;

public interface IAccessControlSecurityProvider
{
    Task<IAccessControlPermission> GrantPermissionAsync(IAccessControlPermissionGrant permissionGrant);
    Task RevokePermissionAsync(IAccessControlPermissionRevoke permissionRevoke);
    Task<bool> CheckPermissionAsync(IFileSystemUser user, IFileSystemResource resource, FileSystemSecuredOperation operation);
    Task<IEnumerable<IAccessControlPermission>> GetPermissionsAsync(IFileSystemResource resource);
}

public interface IAccessControlPermissionRevoke
{
}

public interface IAccessControlPermissionGrant
{
}