using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IRolePermissionStore<PlatformUser, Guid>
{
    bool UserHasPermission(Guid userId, PermissionName permissionName);
    bool UserHasPermissions(Guid userId, IEnumerable<PermissionName> permissionName);
    IEnumerable<PermissionName> ListUserPermissions(Guid userId);
    IEnumerable<RoleName> ListUserRoles(Guid userId);
    void AssignPermissionToRole(RoleName role, PermissionName permissionName);
    void RemovePermissionFromRole(RoleName role, PermissionName permissionName);
    void AssignRoleToUser(Guid userId, RoleName role);
    void RemoveRoleFromUser(Guid userId, RoleName role);
}
