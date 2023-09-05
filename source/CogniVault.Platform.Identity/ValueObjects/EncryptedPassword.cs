using CogniVault.Platform.Core.Entities;
using CogniVault.Platform.Identity.Abstractions;

namespace CogniVault.Platform.Identity.ValueObjects;

public class EncryptedPassword : IValueObject<EncryptedPassword>
{
    public string Value { get; private set; }
    private readonly IPasswordEncryptor _encryptor;

    private EncryptedPassword(string encryptedValue, IPasswordEncryptor encryptor)
    {
        Value = encryptedValue;
        _encryptor = encryptor;
    }

    public int CompareTo(EncryptedPassword? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public EncryptedPassword Copy()
    {
        return new EncryptedPassword(Value, _encryptor);
    }

    public bool Equals(EncryptedPassword? other)
    {
        return Value == other?.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public bool Verify(PlainPassword passwordToVerify)
    {
        return _encryptor.Verify(this, passwordToVerify);
    }

    public static async Task<EncryptedPassword> CreateAsync(PlainPassword plainPassword, IPasswordEncryptor encryptor)
    {
        var encryptedValue = encryptor.Encrypt(plainPassword);
        return new EncryptedPassword(encryptedValue, encryptor);
    }

    public static EncryptedPassword Null => new EncryptedPassword(string.Empty, null);
}
