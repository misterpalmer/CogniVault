using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

using FluentValidation.Results;

namespace CogniVault.Application.Tests.ValueTypes;

public class OperationNameTests
{
    private OperationNameValidator _validator;

    public OperationNameTests()
    {
        _validator = new OperationNameValidator();
    }

    [Theory]
    [InlineData("Operation1")]
    [InlineData("AnotherOperation")]
    public void OperationName_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange
        var operationName = new OperationName(value);

        // Act
        ValidationResult results = _validator.Validate(operationName);

        // Assert
        results.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("InvalidOperationName")]
    public void OperationName_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange
        var operationName = new OperationName(value);

        // Act
        ValidationResult results = _validator.Validate(operationName);

        // Assert
        results.IsValid.Should().BeFalse();
    }

    [Fact]
    public void OperationName_Equals_ShouldReturnTrueForEqualOperationNames()
    {
        // Arrange
        var operationName1 = new OperationName("Operation1");
        var operationName2 = new OperationName("Operation1");

        // Act
        bool result = operationName1.Equals(operationName2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OperationName_Equals_ShouldReturnFalseForDifferentOperationNames()
    {
        // Arrange
        var operationName1 = new OperationName("Operation1");
        var operationName2 = new OperationName("AnotherOperation");

        // Act
        bool result = operationName1.Equals(operationName2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperationName_Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var operationName = new OperationName("Operation1");

        // Act
        bool result = operationName.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperationName_Equals_ShouldReturnFalseForDifferentObjectType()
    {
        // Arrange
        var operationName = new OperationName("Operation1");
        var otherObject = new object();

        // Act
        bool result = operationName.Equals(otherObject);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperationName_GetHashCode_ShouldReturnSameHashCodeForEqualOperationNames()
    {
        // Arrange
        var operationName1 = new OperationName("Operation1");
        var operationName2 = new OperationName("Operation1");

        // Act
        int hashCode1 = operationName1.GetHashCode();
        int hashCode2 = operationName2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void OperationName_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var operationName = new OperationName("Operation1");

        // Act
        string result = operationName.ToString();

        // Assert
        result.Should().Be("Operation1");
    }
}
