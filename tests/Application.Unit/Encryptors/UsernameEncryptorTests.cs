namespace CogniVault.Application.Encryptors.UnitTests;

public class UsernameEncryptorTests
{
    private readonly UsernameEncryptor _usernameEncryptor;

    public UsernameEncryptorTests()
    {
        _usernameEncryptor = new UsernameEncryptor();
    }

    [Fact]
    public void Encrypt_GivenValidPassword_ReturnsSaltAndPasswordSeparatedByColon()
    {
        // Arrange
        var username = "testpassword";

        // Act
        var encryptedUsername = _usernameEncryptor.Encrypt(username);

        // Assert
        encryptedUsername.Should().Contain(":").And.NotBeNullOrEmpty();
    }

    [Fact]
    public void Verify_GivenValidPasswordAndHash_ReturnsTrue()
    {
        // Arrange
        var username = "testpassword";
        var hashedUsername = _usernameEncryptor.Encrypt(username);

        // Act
        var isValid = _usernameEncryptor.Verify(hashedUsername, username);

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void Verify_GivenInvalidPasswordAndHash_ReturnsFalse()
    {
        // Arrange
        var username = "testpassword";
        var hashedUsername = _usernameEncryptor.Encrypt(username);
        var wrongUsername = "wrongpassword";

        // Act
        var isValid = _usernameEncryptor.Verify(hashedUsername, wrongUsername);

        // Assert
        isValid.Should().BeFalse();
    }
}