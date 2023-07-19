namespace CogniVault.Application.Encryptors.UnitTests;


public class PasswordEncryptorTests
{
    private readonly PasswordEncryptor _passwordEncryptor;

    public PasswordEncryptorTests()
    {
        _passwordEncryptor = new PasswordEncryptor();
    }

    [Fact]
    public void Encrypt_GivenValidPassword_ReturnsSaltAndPasswordSeparatedByColon()
    {
        // Arrange
        var password = "testpassword";

        // Act
        var encryptedPassword = _passwordEncryptor.Encrypt(password);

        // Assert
        encryptedPassword.Should().Contain(":").And.NotBeNullOrEmpty();
    }

    [Fact]
    public void Verify_GivenValidPasswordAndHash_ReturnsTrue()
    {
        // Arrange
        var password = "testpassword";
        var hashedPassword = _passwordEncryptor.Encrypt(password);

        // Act
        var isValid = _passwordEncryptor.Verify(hashedPassword, password);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void Verify_GivenInvalidPasswordAndHash_ReturnsFalse()
    {
        // Arrange
        var password = "testpassword";
        var hashedPassword = _passwordEncryptor.Encrypt(password);
        var wrongPassword = "wrongpassword";

        // Act
        var isValid = _passwordEncryptor.Verify(hashedPassword, wrongPassword);

        // Assert
        isValid.Should().BeFalse();
    }
}

