namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IUsernameEncryptor
{
    string Encrypt(string username);
    bool Verify(string username, string encryptedUsername);
}