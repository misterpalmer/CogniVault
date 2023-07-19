using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IUser
{
    Guid Id { get; set; }
    Username Username { get; }
    Email? Email { get; }
    void ChangeUsername(Username newUsername);
    void ChangeEmail(Email newEmail);
}
