using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;


namespace CogniVault.Application.Entities.UnitTests;


public class SuperUserTests : IDisposable
{
    private readonly Mock<IUsernameValidator> _usernameValidatorMock;
    private readonly Mock<IPasswordValidator> _passwordValidatorMock;
    private readonly Mock<IPasswordEncryptor> _passwordEncryptorMock;
    private readonly Mock<ITimeProvider> _timeProviderMock;
    private readonly Username _username;
    private readonly Password _password;
    private readonly TimeZoneInfo _timeZoneInfo;
    private readonly int _quota;
    private SuperUser _superUser;

    public SuperUserTests()
    {
        _usernameValidatorMock = new Mock<IUsernameValidator>();
        _passwordValidatorMock = new Mock<IPasswordValidator>();
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
        _superUser = new SuperUser(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota);
    }

    public void Dispose()
    {
        _superUser = null; // Clear the reference to superUser when we are done
    }

    [Fact]
    public void SuperUser_Constructor_ShouldSetPropertiesCorrectly()
    {
        // Assert
        using (var scope = new AssertionScope());
        _superUser.Username.Value.Should().Be("testuser");
        _superUser.Password.Value.Should().Be("password");
        _superUser.TimeZone.Should().Be(_timeZoneInfo);
        _superUser.IsSuperUser.Should().BeTrue();
        _superUser.Quota.Should().Be(_quota);
        _superUser.Id.Should().NotBeEmpty();
        _superUser.LastLoginAt.Should().BeCloseTo(_superUser.CreatedAt, precision: TimeSpan.FromMilliseconds(1000));
        _superUser.LastLoginAt.Should().BeCloseTo(_superUser.UpdatedAt, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void Should_Be_SuperUser()
    {
        // Assert
        _superUser.IsSuperUser.Should().BeTrue();
    }

    [Fact]
    public void Should_Perform_SuperUser_Action()
    {
        // You would need to implement and check specific actions in PerformSuperUserAction method
        // Here we're simply checking that we can call the method without it throwing an exception

        // Act
        Action act = () => _superUser.PerformSuperUserAction();

        // Assert
        act.Should().NotThrow();
    }
}


