using CogniVault.Application.Entities.AccessControl;
using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.Entities;

// public class FileSystemUserTests
// {
//     private readonly Mock<UsernameValidator> _usernameValidatorMock;
//     private readonly Mock<PasswordValidator> _passwordValidatorMock;
//     private readonly Mock<PasswordEncryptor> _passwordEncryptorMock;
//     private readonly Mock<ITimeProvider> _timeProviderMock;
//     private readonly Username _username;
//     private readonly Password _password;
//     private readonly TimeZoneInfo _timeZoneInfo;
//     private readonly int _quota;

//     public FileSystemUserTests()
//     {
//         _usernameValidatorMock = new Mock<UsernameValidator>();
//         _passwordValidatorMock = new Mock<PasswordValidator>();
//         _passwordEncryptorMock = new Mock<PasswordEncryptor>();
//         _timeProviderMock = new Mock<ITimeProvider>();

//         _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
//         _passwordValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
//         _passwordEncryptorMock.Setup(e => e.Encrypt(It.IsAny<string>())).Returns((string s) => s);
//         _timeProviderMock.Setup(tp => tp.UtcNow).Returns(DateTime.UtcNow);

//         _username = new Username("testuser", _usernameValidatorMock.Object);
//         _password = new Password("password", _passwordValidatorMock.Object, _passwordEncryptorMock.Object);
//         _timeZoneInfo = TimeZoneInfo.Utc;
//         _quota = 10;
//     }

//     public void Dispose()
//     {
//         // Dispose resources if needed
//     }

//     [Fact]
//     public void FileSystemUser_Constructor_ShouldSetPropertiesCorrectly()
//     {
//         // Act
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;

//         // Assert
//         using (var scope = new AssertionScope());
//         FileSystemUser.Username.Value.Should().Be("testuser");
//         FileSystemUser.Password.Value.Should().Be("password");
//         FileSystemUser.TimeZone.Should().Be(_timeZoneInfo);
//         FileSystemUser.Quota.Should().Be(_quota);
//         FileSystemUser.Id.Should().NotBeEmpty();
//         FileSystemUser.LastLoginAt.Should().BeCloseTo(FileSystemUser.CreatedAt, precision: TimeSpan.FromMilliseconds(1000));
//         FileSystemUser.LastLoginAt.Should().BeCloseTo(FileSystemUser.UpdatedAt, precision: TimeSpan.FromMilliseconds(1000));
//     }

//     [Fact]
//     public void FileSystemUser_ModifyLastLoginAt_ShouldUpdateLastLoginAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var lastLoginAt = DateTime.UtcNow;

//         // Act
//         FileSystemUser.ModifyLastLoginAt(lastLoginAt);

//         // Assert
//         FileSystemUser.LastLoginAt.Should().Be(lastLoginAt);
//     }

//     [Fact]
//     public void FileSystemUser_ModifyUpdatedAt_ShouldUpdateUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var updatedAt = DateTime.UtcNow;

//         // Act
//         FileSystemUser.ModifyUpdatedAt(updatedAt);

//         // Assert
//         FileSystemUser.UpdatedAt.Should().Be(updatedAt);
//     }

//     [Fact]
//     public void FileSystemUser_ChangeUsername_ShouldUpdateUsernameAndUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var newUsernameMock = new Mock<Username>("newuser", _usernameValidatorMock.Object);
        
//         // Act
//         FileSystemUser.ChangeUsername(newUsernameMock.Object);

//         // Assert
//         FileSystemUser.Username.Should().BeEquivalentTo(newUsernameMock.Object);
//         FileSystemUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
//     }

//     [Fact]
//     public void FileSystemUser_ChangePassword_ShouldUpdatePasswordAndUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var newPasswordMock = new Mock<Password>("newpassword", _passwordValidatorMock.Object, _passwordEncryptorMock.Object);

//         // Act
//         FileSystemUser.ChangePassword(newPasswordMock.Object);

//         // Assert
//         FileSystemUser.Password.Should().BeEquivalentTo(newPasswordMock.Object);
//         FileSystemUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
//     }

//     [Fact]
//     public void FileSystemUser_ChangeEmail_ShouldUpdateEmailAndUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var newEmail = new Email("test@example.com");

//         // Act
//         FileSystemUser.ChangeEmail(newEmail);

//         // Assert
//         FileSystemUser.Email.Should().Be(newEmail);
//         FileSystemUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
//     }

//     [Fact]
//     public void FileSystemUser_ChangeQuota_ShouldUpdateQuotaAndUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var newQuota = new Quota(2000);

//         // Act
//         FileSystemUser.ChangeQuota(newQuota);

//         // Assert
//         FileSystemUser.Quota.Should().Be(newQuota);
//         FileSystemUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
//     }

//     [Fact]
//     public void FileSystemUser_ChangeTimeZone_ShouldUpdateTimeZoneAndUpdatedAt()
//     {
//         // Arrange
//         var FileSystemUser = new Mock<FileSystemUser>(_timeProviderMock.Object, _username, _password, _timeZoneInfo, _quota) { CallBase = true }.Object;
//         var newTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

//         // Act
//         FileSystemUser.ChangeTimeZone(newTimeZone);

//         // Assert
//         FileSystemUser.TimeZone.Should().Be(newTimeZone);
//         // FileSystemUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromMilliseconds(1000));
//     }
// }


// public class UserTests : IDisposable
// {
//     private readonly Mock<UsernameValidator> _usernameValidatorMock;
//     private readonly Mock<PasswordValidator> _passwordValidatorMock;
//     private readonly Mock<PasswordEncryptor> _passwordEncryptorMock;
//     private readonly Mock<ITimeProvider> _timeProviderMock;
//     private readonly Username _username;
//     private readonly Password _password;
//     private readonly TimeZoneInfo _timeZoneInfo;
//     private readonly Quota _quota;
//     private FileSystemUser _user;

//     public UserTests()
//     {
//         _usernameValidatorMock = new Mock<UsernameValidator>();
//         _passwordValidatorMock = new Mock<PasswordValidator>();
//         _passwordEncryptorMock = new Mock<PasswordEncryptor>();
//         _timeProviderMock = new Mock<ITimeProvider>();

//         _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
//         _passwordValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
//         _passwordEncryptorMock.Setup(e => e.Encrypt(It.IsAny<string>())).Returns((string s) => s);
//         _timeProviderMock.Setup(tp => tp.UtcNow).Returns(DateTime.UtcNow);

//         _username = new Username("testuser", _usernameValidatorMock.Object);
//         _password = new Password("password", _passwordValidatorMock.Object, _passwordEncryptorMock.Object);
//         _timeZoneInfo = TimeZoneInfo.Utc;
//         _quota = new Quota(1000);

//         // Setting up the User object for all tests to use.
//         _user = new FileSystemUser(_timeProviderMock.Object, _username, _password, TimeZoneInfo.Utc, _quota);
//     }

//     public void Dispose()
//     {
//         _user = null; // Clear the reference to user when we are done
//     }

//     [Fact]
//     public void User_Should_Update_Username()
//     {
//         var newUsername = new Username("newusername", _usernameValidatorMock.Object);
//         _timeProviderMock.Setup(m => m.Now).Returns(DateTime.UtcNow); // Update the return value for the mock

//         // Act
//         _user.ChangeUsername(newUsername);

//         // Assert
//         _user.Username.Should().Be(newUsername);
//     }
// }
