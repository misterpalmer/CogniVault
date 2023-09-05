using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IUserAuthentication
{
    EncryptedPassword Password { get; }
    void ChangePassword(EncryptedPassword newPassword);
}