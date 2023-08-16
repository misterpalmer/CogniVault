using CogniVault.Application.Abstractions.Resources.AccessControl.Groups;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;

namespace CogniVault.Application.Entities;

public class AccessControlResourceType : ResourceType
{
    public static readonly AccessControlResourceType User = new AccessControlResourceType(typeof(IFileSystemUser));
    public static readonly AccessControlResourceType Group = new AccessControlResourceType(typeof(IFileSystemGroup));

    public AccessControlResourceType(Type type) : base(type) {}

    public override ResourceType FromName(string resourceTypeName) => resourceTypeName.ToUpper() switch
    {
        "USER" or "U" => AccessControlResourceType.User,
        "GROUP" or "G" => AccessControlResourceType.Group,
        _ => throw new NotSupportedException($"AccessControlResourceType_{resourceTypeName}"),
    };
}