using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.Abstractions;

public interface IPasswordEncryptor
{
    string Encrypt(PlainPassword password);
    bool Verify(EncryptedPassword password, PlainPassword hashedPassword);
}