using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Stores;

public class RolePermissionInMemoryStore<T, TId> : IRolePermissionStore<T, TId> where T : PlatformUser where TId : struct
{
    private readonly Dictionary<TId, List<RoleName>> _userRoles = new();
    private readonly Dictionary<RoleName, List<PermissionName>> _rolePermissions = new();

    public bool UserHasPermission(TId userId, PermissionName permissionName)
    {
        return ListUserPermissions(userId).Contains(permissionName);
    }

    public bool UserHasPermissions(TId userId, IEnumerable<PermissionName> permissions)
    {
        var userPermissions = ListUserPermissions(userId);
        return permissions.All(p => userPermissions.Contains(p));
    }

    public IEnumerable<PermissionName> ListUserPermissions(TId userId)
    {
        if (!_userRoles.TryGetValue(userId, out var roles))
            return Enumerable.Empty<PermissionName>();

        return roles.SelectMany(role => _rolePermissions.GetValueOrDefault(role, new List<PermissionName>())).Distinct();
    }

    public IEnumerable<RoleName> ListUserRoles(TId userId)
    {
        return _userRoles.GetValueOrDefault(userId, new List<RoleName>());
    }

    public void AssignPermissionToRole(RoleName role, PermissionName permissionName)
    {
        if (!_rolePermissions.ContainsKey(role))
            _rolePermissions[role] = new List<PermissionName>();

        if (!_rolePermissions[role].Contains(permissionName))
            _rolePermissions[role].Add(permissionName);
    }

    public void RemovePermissionFromRole(RoleName role, PermissionName permissionName)
    {
        _rolePermissions.GetValueOrDefault(role)?.Remove(permissionName);
    }

    public void AssignRoleToUser(TId userId, RoleName role)
    {
        if (!_userRoles.ContainsKey(userId))
            _userRoles[userId] = new List<RoleName>();

        if (!_userRoles[userId].Contains(role))
            _userRoles[userId].Add(role);
    }

    public void RemoveRoleFromUser(TId userId, RoleName role)
    {
        _userRoles.GetValueOrDefault(userId)?.Remove(role);
    }
}

