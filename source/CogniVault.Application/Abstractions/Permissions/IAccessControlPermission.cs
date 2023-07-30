using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.AccessControl.Groups;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.Constants;

namespace CogniVault.Application.Abstractions.Permissions;

public interface IAccessControlPermission
{
    Guid Id { get; set; }
    PermissionType PermissionType { get; set; }
    IFileSystemUser User { get; set; }
    IFileSystemGroup Group { get; set; }
    IResource Resource { get; set; }
    DateTime GrantedOn { get; set; }
    IFileSystemUser GrantedBy { get; set; }
    bool Allows(FileSystemSecuredOperation operation);
}
