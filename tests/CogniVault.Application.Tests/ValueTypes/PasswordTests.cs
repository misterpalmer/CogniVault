using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;
using CogniVault.Platform.Identity.Provider;

using FluentValidation;

namespace CogniVault.Application.Tests.ValueTypes;

public class PasswordTests
{
    private readonly Mock<PasswordEncryptor> _encryptorMock;
    private readonly PasswordValidator _validator;

    public PasswordTests()
    {
        _encryptorMock = new Mock<PasswordEncryptor>();
        _validator = new PasswordValidator();
    }

    [Theory]
    [InlineData("Password123!")]
    [InlineData("MySecurePassword$1")]
    public void Password_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange
        _encryptorMock.Setup(e => e.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // Act
        var password = new Password(value, _encryptorMock.Object);
        var validationResult = _validator.Validate(password);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalid")]
    [InlineData("NOUPPERCASE1!")]
    [InlineData("nolowercase1!")]
    [InlineData("NoDigit!")]
    [InlineData("NoSpecialChar1")]
    public void Password_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange
        Action act = () => new Password(value, _encryptorMock.Object);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Password_Equals_ShouldReturnTrueForEqualPasswords()
    {
        // Arrange
        _encryptorMock.Setup(e => e.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        var password1 = new Password("TestPassword1!", _encryptorMock.Object);
        var password2 = new Password("TestPassword1!", _encryptorMock.Object);

        // Act
        bool result = password1.Equals(password2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Password_Equals_ShouldReturnFalseForDifferentPasswords()
    {
        // Arrange
        _encryptorMock.Setup(e => e.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        var password1 = new Password("TestPassword1!", _encryptorMock.Object);
        var password2 = new Password("DifferentPassword2!", _encryptorMock.Object);

        // Act
        bool result = password1.Equals(password2);

        // Assert
        result.Should().BeFalse();
    }
}
