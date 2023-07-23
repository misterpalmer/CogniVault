using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IGroup
{
    Guid Id { get; }
    GroupName Name { get; set; }
    ICollection<IUser> Users { get; }

    void AddUser(IUser user);
    void RemoveUser(IUser user);
    void Rename(GroupName newName);
}
