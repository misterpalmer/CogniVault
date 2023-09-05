using CogniVault.Application.ValueObjects;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IFileSystemUser : IAccessControlEntity
{
    Username Username { get; }
    Email? Email { get; }
    void ChangeUsername(Username newUsername);
    void ChangeEmail(Email newEmail);
}
