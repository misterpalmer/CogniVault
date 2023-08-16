namespace CogniVault.Application.Abstractions.Permissions;

public interface IAccessControlPermissionNode
{
    Guid Id { get; set; }
    IAccessControlPermission Permission { get; set; }
    List<IAccessControlPermissionNode> Children { get; set; }
}