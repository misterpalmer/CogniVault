using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

using FluentValidation.Results;

namespace CogniVault.Application.Tests.ValueObjects;

public class PermissionNameTests
{
    private PermissionNameValidator _validator;

    public PermissionNameTests()
    {
        _validator = new PermissionNameValidator();
    }

    [Theory]
    [InlineData("Operation1")]
    [InlineData("AnotherOperation")]
    public void PermissionName_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange
        var permissionName = new PermissionName(value);

        // Act
        ValidationResult results = _validator.Validate(permissionName);

        // Assert
        results.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("InvalidPermissionName")]
    public void PermissionName_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange
        var permissionName = new PermissionName(value);

        // Act
        ValidationResult results = _validator.Validate(permissionName);

        // Assert
        results.IsValid.Should().BeFalse();
    }

    [Fact]
    public void PermissionName_Equals_ShouldReturnTrueForEqualPermissionNames()
    {
        // Arrange
        var PermissionName1 = new PermissionName("Operation1");
        var PermissionName2 = new PermissionName("Operation1");

        // Act
        bool result = PermissionName1.Equals(PermissionName2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void PermissionName_Equals_ShouldReturnFalseForDifferentPermissionNames()
    {
        // Arrange
        var PermissionName1 = new PermissionName("Operation1");
        var PermissionName2 = new PermissionName("AnotherOperation");

        // Act
        bool result = PermissionName1.Equals(PermissionName2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void PermissionName_Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var PermissionName = new PermissionName("Operation1");

        // Act
        bool result = PermissionName.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void PermissionName_Equals_ShouldReturnFalseForDifferentObjectType()
    {
        // Arrange
        var PermissionName = new PermissionName("Operation1");
        var otherObject = new object();

        // Act
        bool result = PermissionName.Equals(otherObject);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void PermissionName_GetHashCode_ShouldReturnSameHashCodeForEqualPermissionNames()
    {
        // Arrange
        var PermissionName1 = new PermissionName("Operation1");
        var PermissionName2 = new PermissionName("Operation1");

        // Act
        int hashCode1 = PermissionName1.GetHashCode();
        int hashCode2 = PermissionName2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void PermissionName_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var PermissionName = new PermissionName("Operation1");

        // Act
        string result = PermissionName.ToString();

        // Assert
        result.Should().Be("Operation1");
    }
}
