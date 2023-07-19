using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities.UnitTests;

public class BaseUserTests
{
    private readonly Mock<IUsernameValidator> _usernameValidatorMock;
    private readonly Mock<IPasswordValidator> _passwordValidatorMock;
    private readonly Mock<IPasswordEncryptor> _passwordEncryptorMock;
    private readonly Mock<ITimeProvider> _timeProviderMock;
    private readonly Username _username;
    private readonly Password _password;
    private readonly TimeZoneInfo _timeZoneInfo;
    private readonly int _quota;

    public BaseUserTests()
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
    }

    public void Dispose()
    {
        // Dispose resources if needed
    }

    [Fact]
    public void BaseUser_Constructor_ShouldSetPropertiesCorrectly()
    {
        // Act
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;

        // Assert
        using (var scope = new AssertionScope());
        baseUser.Username.Value.Should().Be("testuser");
        baseUser.Password.Value.Should().Be("password");
        baseUser.TimeZone.Should().Be(_timeZoneInfo);
        baseUser.IsSuperUser.Should().BeFalse();
        baseUser.Quota.Should().Be(_quota);
        baseUser.Id.Should().NotBeEmpty();
        baseUser.LastLoginAt.Should().BeCloseTo(baseUser.CreatedAt, precision: TimeSpan.FromMilliseconds(1000));
        baseUser.LastLoginAt.Should().BeCloseTo(baseUser.UpdatedAt, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void BaseUser_ModifyLastLoginAt_ShouldUpdateLastLoginAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var lastLoginAt = DateTime.UtcNow;

        // Act
        baseUser.ModifyLastLoginAt(lastLoginAt);

        // Assert
        baseUser.LastLoginAt.Should().Be(lastLoginAt);
    }

    [Fact]
    public void BaseUser_ModifyUpdatedAt_ShouldUpdateUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var updatedAt = DateTime.UtcNow;

        // Act
        baseUser.ModifyUpdatedAt(updatedAt);

        // Assert
        baseUser.UpdatedAt.Should().Be(updatedAt);
    }

    [Fact]
    public void BaseUser_ChangeUsername_ShouldUpdateUsernameAndUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var newUsernameMock = new Mock<Username>("newuser", _usernameValidatorMock.Object);
        
        // Act
        baseUser.ChangeUsername(newUsernameMock.Object);

        // Assert
        baseUser.Username.Should().BeEquivalentTo(newUsernameMock.Object);
        baseUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void BaseUser_ChangePassword_ShouldUpdatePasswordAndUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var newPasswordMock = new Mock<Password>("newpassword", _passwordValidatorMock.Object, _passwordEncryptorMock.Object);

        // Act
        baseUser.ChangePassword(newPasswordMock.Object);

        // Assert
        baseUser.Password.Should().BeEquivalentTo(newPasswordMock.Object);
        baseUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void BaseUser_ChangeEmail_ShouldUpdateEmailAndUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var newEmail = new Email("test@example.com");

        // Act
        baseUser.ChangeEmail(newEmail);

        // Assert
        baseUser.Email.Should().Be(newEmail);
        baseUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void BaseUser_ChangeQuota_ShouldUpdateQuotaAndUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var newQuota = 20;

        // Act
        baseUser.ChangeQuota(newQuota);

        // Assert
        baseUser.Quota.Should().Be(newQuota);
        baseUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void BaseUser_ChangeTimeZone_ShouldUpdateTimeZoneAndUpdatedAt()
    {
        // Arrange
        var baseUser = new Mock<BaseUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
        var newTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

        // Act
        baseUser.ChangeTimeZone(newTimeZone);

        // Assert
        baseUser.TimeZone.Should().Be(newTimeZone);
        // baseUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
    }
}