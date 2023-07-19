using CogniVault.Application.Interfaces;


namespace CogniVault.Application.ValueObjects.UnitTests;

public class PasswordTests
{
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenEncryptorIsNull()
    {
        // Arrange
        IPasswordEncryptor encryptor = null;
        var validatorMock = new Mock<IPasswordValidator>();
        var value = "password";
        // Act
        var action = () => new Password(value, validatorMock.Object, encryptor);
        // Assert
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be("encryptor");
    }
    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenValidatorIsNull()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        IPasswordValidator validator = null;
        var value = "password";
        // Act
        var action = () => new Password(value, validator, encryptorMock.Object);
        // Assert
        action.Should().Throw<ArgumentNullException>()
            .And.ParamName.Should().Be("validator");
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_ThrowsArgumentException_WhenValueIsNullOrWhitespace(string value)
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();
        // Act
        var action = () => new Password(value, validatorMock.Object, encryptorMock.Object);
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Invalid password (Parameter 'value')")
            .And.ParamName.Should().Be("value");
    }
    [Fact]
    public void Constructor_ThrowsArgumentException_WhenValueFailsValidation()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(false);
        var value = "password";
        // Act
        var action = () => new Password(value, validatorMock.Object, encryptorMock.Object);
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Invalid password (Parameter 'value')")
            .And.ParamName.Should().Be("value");
    }
    [Fact]
    public void Constructor_EncryptsValue_UsingEncryptor()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var value = "password";
        var encryptedValue = "encryptedPassword";
        encryptorMock.Setup(e => e.Encrypt(value)).Returns(encryptedValue);
        // Act
        var password = new Password(value, validatorMock.Object, encryptorMock.Object);
        // Assert
        password.Value.Should().Be(encryptedValue);
        encryptorMock.Verify(e => e.Encrypt(value), Times.Once);
    }
    [Theory]
    [InlineData("password", true)]
    [InlineData("wrongpassword", false)]
    public void Verify_ReturnsExpectedResult(string passwordToVerify, bool expectedResult)
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();

        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);

        var value = "password";
        var encryptedPassword = "encryptedPassword";

        encryptorMock.Setup(e => e.Encrypt(value)).Returns(encryptedPassword);
        encryptorMock.Setup(e => e.Verify(passwordToVerify, encryptedPassword)).Returns(expectedResult);

        var password = new Password(value, validatorMock.Object, encryptorMock.Object);

        // Act
        var result = password.Verify(passwordToVerify);

        // Assert
        result.Should().Be(expectedResult);
        encryptorMock.Verify(e => e.Verify(passwordToVerify, encryptedPassword), Times.Once);
    }
    [Fact]
    public void Equals_ReturnsTrue_WhenPasswordsAreEqual()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var value = "password";
        var password1 = new Password(value, validatorMock.Object, encryptorMock.Object);
        var password2 = new Password(value, validatorMock.Object, encryptorMock.Object);
        // Act
        var result = password1.Equals(password2);
        // Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void Equals_ReturnsFalse_WhenOtherIsNull()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        var validatorMock = new Mock<IPasswordValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);
        var value = "password";
        var password = new Password(value, validatorMock.Object, encryptorMock.Object);
        // Act
        var result = password.Equals(null);
        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public void Equals_ReturnsFalse_WhenPasswordsAreDifferent()
    {
        // Arrange
        var encryptorMock = new Mock<IPasswordEncryptor>();
        encryptorMock.Setup(e => e.Encrypt("password1")).Returns("encryptedPassword1");
        encryptorMock.Setup(e => e.Encrypt("password2")).Returns("encryptedPassword2");

        var validatorMock = new Mock<IPasswordValidator>();
        validatorMock.Setup(v => v.Validate(It.IsAny<string>())).Returns(true);

        var value1 = "password1";
        var value2 = "password2";
        var password1 = new Password(value1, validatorMock.Object, encryptorMock.Object);
        var password2 = new Password(value2, validatorMock.Object, encryptorMock.Object);

        // Act
        var result = password1.Equals(password2);

        // Assert
        result.Should().BeFalse();
    }
}