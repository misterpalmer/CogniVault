namespace CogniVault.Platform.Identity.Abstractions;

public interface IPasswordEncryptor
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}