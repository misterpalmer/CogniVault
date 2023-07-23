using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IUsernameEncryptor
{
    string Encrypt(string username);
    bool Verify(string username, string encryptedUsername);
}