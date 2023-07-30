namespace CogniVault.Application.Constants;

/// <summary>
/// With a flags enum, enum members can be combined
/// using the bitwise OR operator.
/// For example, PermissionType.Read | PermissionType.Write
// represents both read and write permissions.
/// </summary>
[Flags]
public enum PermissionType
{
    NoAccess = 0,  // No permissions
    Read = 1 << 0, // Read permission
    Write = 1 << 1, // Write permission
    Execute = 1 << 2 // Execute permission
}