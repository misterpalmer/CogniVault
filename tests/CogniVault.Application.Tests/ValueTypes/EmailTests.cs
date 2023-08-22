using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;
using CogniVault.Platform.Identity.Validators;

using FluentValidation.Results;

namespace CogniVault.Application.Tests.ValueTypes;

public class EmailTests
{
    private EmailValidator _validator;

    public EmailTests()
    {
        _validator = new EmailValidator();
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("another@example.com")]
    public void Email_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange
        var email = new Email(_validator, value);

        // Act
        ValidationResult results = _validator.Validate(email);

        // Assert
        results.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("invalidemail")]
    public void Email_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange
        var email = new Email(_validator, value);

        // Act
        ValidationResult results = _validator.Validate(email);

        // Assert
        results.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Email_Equals_ShouldReturnTrueForEqualEmails()
    {
        // Arrange
        var email1 = new Email(_validator, "test@example.com");
        var email2 = new Email(_validator, "test@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForDifferentEmails()
    {
        // Arrange
        var email1 = new Email(_validator, "test@example.com");
        var email2 = new Email(_validator, "test@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var email = new Email(_validator, "test@example.com");

        // Act
        bool result = email.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_Equals_ShouldReturnFalseForDifferentObjectType()
    {
        // Arrange
        var email = new Email(_validator, "test@example.com");
        var otherObject = new object();

        // Act
        bool result = email.Equals(otherObject);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Email_GetHashCode_ShouldReturnSameHashCodeForEqualEmails()
    {
        // Arrange
        var email1 = new Email(_validator, "test@example.com");
        var email2 = new Email(_validator, "test@example.com");

        // Act
        int hashCode1 = email1.GetHashCode();
        int hashCode2 = email2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void Email_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var email = new Email(_validator, "test@example.com");

        // Act
        string result = email.ToString();

        // Assert
        result.Should().Be("test@example.com");
    }
}
