using CogniVault.Application.Abstractions.Resources.AccessControl;
using CogniVault.Application.Abstractions.Resources.AccessControl.Groups;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
using CogniVault.Application.ValueObjects;


namespace CogniVault.Application.Entities.AccessControl;

public class FileSystemGroup : IFileSystemGroup
{
    public Guid Id { get; }
    public GroupName Name { get; set; }
    public ICollection<IFileSystemUser> Users { get; }

    ResourceName IAccessControlEntity.Name => throw new NotImplementedException();

    public FileSystemGroup(GroupName name)
    {
        Name = name;
        Users = new List<IFileSystemUser>();
    }

    public void AddUser(IFileSystemUser user)
    {
        Users.Add(user);
    }

    public void RemoveUser(IFileSystemUser user)
    {
        Users.Remove(user);
    }

    public void Rename(GroupName newName)
    {
        Name = newName;
    }
}
