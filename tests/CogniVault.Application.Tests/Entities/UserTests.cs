using CogniVault.Application.Entities;
using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.Entities;

public class UserTests : IDisposable
{
    private readonly Mock<UsernameValidator> _usernameValidatorMock;
    private readonly Mock<PasswordValidator> _passwordValidatorMock;
    private readonly Mock<IPasswordEncryptor> _passwordEncryptorMock;
    private readonly Mock<ITimeProvider> _timeProviderMock;
    private readonly Username _username;
    private readonly Password _password;
    private readonly TimeZoneInfo _timeZoneInfo;
    private readonly int _quota;
    private User _user;

    public UserTests()
    {
        _usernameValidatorMock = new Mock<UsernameValidator>();
        _passwordValidatorMock = new Mock<PasswordValidator>();
        _passwordEncryptorMock = new Mock<IPasswordEncryptor>();
        _timeProviderMock = new Mock<ITimeProvider>();

        _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        _passwordValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        _passwordEncryptorMock.Setup(e => e.Encrypt(It.IsAny<string>())).Returns((string s) => s);
        _timeProviderMock.Setup(tp => tp.UtcNow).Returns(DateTime.UtcNow);

        _username = new Username("testuser", _usernameValidatorMock.Object);
        _password = new Password("password", _passwordValidatorMock.Object, _passwordEncryptorMock.Object);
        _timeZoneInfo = TimeZoneInfo.Utc;
        _quota = 10;

        // Setting up the User object for all tests to use.
        _user = new User(_timeProviderMock.Object, _username, _password, TimeZoneInfo.Utc);
    }

    public void Dispose()
    {
        _user = null; // Clear the reference to user when we are done
    }

    [Fact]
    public void User_Should_Not_Be_SuperUser()
    {
        // Assert
        _user.IsSuperUser.Should().BeFalse();
    }

    [Fact]
    public void User_Should_Update_Username()
    {
        var newUsername = new Username("newusername", _usernameValidatorMock.Object);
        _timeProviderMock.Setup(m => m.Now).Returns(DateTime.UtcNow); // Update the return value for the mock

        // Act
        _user.ChangeUsername(newUsername);

        // Assert
        _user.Username.Should().Be(newUsername);
    }
}

