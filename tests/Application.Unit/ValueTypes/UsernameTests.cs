using CogniVault.Application.Interfaces;


namespace CogniVault.Application.ValueObjects.UnitTests;


public class UsernameTests
{
    private readonly Mock<IUsernameValidator> _usernameValidatorMock;

    public UsernameTests()
    {
        _usernameValidatorMock = new Mock<IUsernameValidator>();
    }

    [Fact]
    public void UsernameConstructor_NullValidator_ThrowsArgumentNullException()
    {
        // Arrange
        IUsernameValidator validator = null;

        // Act
        Action act = () => new Username("valid_username", validator);

        // Assert
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("validator");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void UsernameConstructor_InvalidUsername_ThrowsArgumentException(string username)
    {
        // Arrange
        _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(false);

        // Act
        Action act = () => new Username(username, _usernameValidatorMock.Object);

        // Assert
        act.Should().Throw<ArgumentException>().And.ParamName.Should().Be("value");
    }

    [Fact]
    public void UsernameConstructor_ValidUsername_SetsValueProperty()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);

        // Act
        var username = new Username(validUsername, _usernameValidatorMock.Object);

        // Assert
        username.Value.Should().Be(validUsername);
    }

    [Fact]
    public void Equals_NullUsername_ReturnsFalse()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);
        var username = new Username(validUsername, _usernameValidatorMock.Object);
        
        // Act
        bool equals = username.Equals((Username)null);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);
        var username1 = new Username(validUsername, _usernameValidatorMock.Object);
        var username2 = new Username(validUsername, _usernameValidatorMock.Object);

        // Act
        bool equals = username1.Equals(username2);

        // Assert
        equals.Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        // Arrange
        _usernameValidatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var username1 = new Username("username1", _usernameValidatorMock.Object);
        var username2 = new Username("username2", _usernameValidatorMock.Object);

        // Act
        bool equals = username1.Equals(username2);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void Equals_ObjectOfDifferentType_ReturnsFalse()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);
        var username = new Username(validUsername, _usernameValidatorMock.Object);
        var notUsername = new object();

        // Act
        bool equals = username.Equals(notUsername);

        // Assert
        equals.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_ReturnsSameHashCodeForSameValue()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);
        var username1 = new Username(validUsername, _usernameValidatorMock.Object);
        var username2 = new Username(validUsername, _usernameValidatorMock.Object);

        // Act
        int hashCode1 = username1.GetHashCode();
        int hashCode2 = username2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void ToString_ReturnsValueProperty()
    {
        // Arrange
        string validUsername = "valid_username";
        _usernameValidatorMock.Setup(v => v.Validate(validUsername)).Returns(true);
        var username = new Username(validUsername, _usernameValidatorMock.Object);

        // Act
        string result = username.ToString();

        // Assert
        result.Should().Be(validUsername);
    }
}
