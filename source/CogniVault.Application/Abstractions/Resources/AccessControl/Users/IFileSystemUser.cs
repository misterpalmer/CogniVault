using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IFileSystemUser : IAccessControlEntity
{
    Guid Id { get; set; }
    Username Username { get; }
    Email? Email { get; }
    void ChangeUsername(Username newUsername);
    void ChangeEmail(Email newEmail);
}
