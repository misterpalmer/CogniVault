using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IRolePermissionStore<T, TId> where T : IPlatformUser<TId> where TId : struct
{
    bool UserHasPermission(TId userId, PermissionName permissionName);
    bool UserHasPermissions(TId userId, IEnumerable<PermissionName> permissionName);
    IEnumerable<PermissionName> ListUserPermissions(TId userId);
    IEnumerable<RoleName> ListUserRoles(TId userId);
    void AssignPermissionToRole(RoleName role, PermissionName permissionName);
    void RemovePermissionFromRole(RoleName role, PermissionName permissionName);
    void AssignRoleToUser(TId userId, RoleName role);
    void RemoveRoleFromUser(TId userId, RoleName role);
}
