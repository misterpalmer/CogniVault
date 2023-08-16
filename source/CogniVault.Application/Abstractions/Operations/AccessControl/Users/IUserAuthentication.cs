using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IUserAuthentication
{
    Password Password { get; }
    void ChangePassword(Password newPassword);
}