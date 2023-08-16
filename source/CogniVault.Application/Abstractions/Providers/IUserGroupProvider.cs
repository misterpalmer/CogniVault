using CogniVault.Application.Abstractions.Resources.AccessControl.Groups;
using CogniVault.Application.Abstractions.Resources.AccessControl.Users;

namespace CogniVault.Application.Abstractions;

public interface IFileSystemUserGroupProvider
{
    // Method to add a user
    Task AddUserAsync(IFileSystemUser user);

    // Method to remove a user
    Task RemoveUserAsync(IFileSystemUser user);

    // Method to add a user to a group
    Task AddUserToGroupAsync(IFileSystemUser user, IFileSystemGroup group);

    // Method to remove a user from a group
    Task RemoveUserFromGroupAsync(IFileSystemUser user, IFileSystemGroup group);

    // Method to check if a user is in a group
    Task<bool> IsUserInGroupAsync(IFileSystemUser user, IFileSystemGroup group);

    // Method to get a user by their username
    Task<IFileSystemUser> GetUserByUsernameAsync(string username);

    // Method to get a group by its name
    Task<IFileSystemGroup> GetGroupByNameAsync(string groupName);
}
