using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities.UnitTests;

public class GroupTests
{
    private readonly Mock<IGroupNameValidator> _groupNameValidatorMock;
    private readonly  Mock<IUsernameValidator> _usernameValidatorMock;
    private readonly  Mock<PasswordValidator> _passwordValidatorMock;
    private readonly  Mock<IPasswordEncryptor> _passwordEncryptorMock;
    private readonly  Mock<ITimeProvider> _timeProviderMock;

    public GroupTests()
    {
        _groupNameValidatorMock = new Mock<IGroupNameValidator>();
        _usernameValidatorMock = new Mock<IUsernameValidator>();
        _passwordValidatorMock = new Mock<PasswordValidator>();
        _passwordEncryptorMock = new Mock<IPasswordEncryptor>();
        _timeProviderMock = new Mock<ITimeProvider>();

        _groupNameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        _passwordValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        _passwordEncryptorMock.Setup(e => e.Encrypt(It.IsAny<string>())).Returns((string s) => s);
        _timeProviderMock.Setup(tp => tp.UtcNow).Returns(DateTime.UtcNow);
    }

    [Theory]
    [InlineData("admins")]
    [InlineData("users")]
    [InlineData("developers")]
    public void Group_Should_Have_Correct_Name(string groupName)
    {
        // Arrange
        var group = new Group(new GroupName(groupName, _groupNameValidatorMock.Object));

        // Assert
        group.Name.Value.Should().Be(groupName);
    }

    [Theory]
    [InlineData("admins", "user1", "password1")]
    [InlineData("admins", "user2", "password2")]
    [InlineData("users", "user3", "password3")]
    public void Group_Should_Contain_Users(string groupName, string userName, string password)
    {
        // Arrange
        var user = new User(_timeProviderMock.Object,
            new Username(userName, _usernameValidatorMock.Object),
            new Password(password, _passwordValidatorMock.Object, _passwordEncryptorMock.Object),
            TimeZoneInfo.Utc
        );

        // Act
        var group = new Group(new GroupName(groupName, _groupNameValidatorMock.Object));
        group.AddUser(user);

        // Assert
        group.Users.Should().Contain(user);
    }

    [Theory]
    [InlineData("admins", "user1", "password1", "user2", "password2")]
    [InlineData("admins", "user1", "password1", "user3", "password3")]
    public void Group_Should_Allow_User_Removal(string groupName, string userName1, string password1, string userName2, string password2)
    {
        // Arrange
        var user1 = new User(_timeProviderMock.Object,
            new Username(userName1, _usernameValidatorMock.Object),
            new Password(password1, _passwordValidatorMock.Object, _passwordEncryptorMock.Object),
            TimeZoneInfo.Utc
        );

        var user2 = new User(_timeProviderMock.Object,
            new Username(userName2, _usernameValidatorMock.Object),
            new Password(password2, _passwordValidatorMock.Object, _passwordEncryptorMock.Object),
            TimeZoneInfo.Utc
        );

        // Act
        var group = new Group(new GroupName(groupName, _groupNameValidatorMock.Object));
        group.AddUser(user1);
        group.AddUser(user2);
        group.RemoveUser(user1);

        // Assert
        group.Users.Should().NotContain(user1);
        group.Users.Should().Contain(user2);
    }
}
