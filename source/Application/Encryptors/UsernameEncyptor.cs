using CogniVault.Application.Interfaces;

using Sodium;

using System.Security.Cryptography;
using System.Text;

public class UsernameEncryptor : IUsernameEncryptor
{
    private const int SaltSize = 32; // This is a typical size for a salt. 

    public string Encrypt(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var salt = GenerateSalt(SaltSize);
        var hashedPassword = HashPassword(passwordBytes, salt);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hashedPassword)}";
    }

    public bool Verify(string combinedSaltAndPassword, string passwordToVerify)
    {
        var passwordParts = combinedSaltAndPassword.Split(':');
        var salt = Convert.FromBase64String(passwordParts[0]);
        var hashedPassword = Convert.FromBase64String(passwordParts[1]);

        var passwordToVerifyBytes = Encoding.UTF8.GetBytes(passwordToVerify);
        var hashedPasswordToVerify = HashPassword(passwordToVerifyBytes, salt);

        return hashedPassword.SequenceEqual(hashedPasswordToVerify);
    }

    private byte[] GenerateSalt(int size)
    {
        var randomNumber = new byte[size];
        RandomNumberGenerator.Fill(randomNumber);

        return randomNumber;
    }

    private byte[] HashPassword(byte[] password, byte[] salt)
    {
        // This method uses the Scrypt algorithm to hash the password.
        return PasswordHash.ScryptHashBinary(password, salt);
    }
}