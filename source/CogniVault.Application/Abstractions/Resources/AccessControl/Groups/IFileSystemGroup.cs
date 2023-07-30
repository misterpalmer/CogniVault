using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl.Groups;

public interface IFileSystemGroup : IAccessControlEntity
{
    Guid Id { get; }
    GroupName Name { get; set; }
    ICollection<IFileSystemUser> Users { get; }

    void AddUser(IFileSystemUser user);
    void RemoveUser(IFileSystemUser user);
    void Rename(GroupName newName);
}
