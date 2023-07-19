namespace CogniVault.Application.Interfaces;


public interface IPasswordEncryptor
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}