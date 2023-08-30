using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.Tests.ValueObjects;

public class FileNameTests
{
    private FileNameValidator _validator;

    public FileNameTests()
    {
        _validator = new FileNameValidator();
    }

    [Theory]
    [InlineData("file.txt")]
    [InlineData("file.pdf")]
    public void FileName_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange
        var fileName = new FileName(value);

        // Act
        ValidationResult results = _validator.Validate(fileName);

        // Assert
        results.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("/invalid/filename")]
    public void FileName_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange & Act
        Action action = () => new FileName(value);

        // Assert
        action.Should().Throw<ValidationException>();
    }

    [Fact]
    public void FileName_Equals_ShouldReturnTrueForEqualFileNames()
    {
        // Arrange
        var fileName1 = new FileName("file.txt");
        var fileName2 = new FileName("file.txt");

        // Act
        bool result = fileName1.Equals(fileName2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void FileName_Equals_ShouldReturnFalseForDifferentFileNames()
    {
        // Arrange
        var fileName1 = new FileName("file1.txt");
        var fileName2 = new FileName("file2.txt");

        // Act
        bool result = fileName1.Equals(fileName2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void FileName_Equals_ShouldReturnFalseForNull()
    {
        // Arrange
        var fileName = new FileName("file.txt");

        // Act
        bool result = fileName.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void FileName_Equals_ShouldReturnFalseForDifferentObjectType()
    {
        // Arrange
        var fileName = new FileName("file.txt");
        var otherObject = new object();

        // Act
        bool result = fileName.Equals(otherObject);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void FileName_GetHashCode_ShouldReturnSameHashCodeForEqualFileNames()
    {
        // Arrange
        var fileName1 = new FileName("file.txt");
        var fileName2 = new FileName("file.txt");

        // Act
        int hashCode1 = fileName1.GetHashCode();
        int hashCode2 = fileName2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void FileName_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var fileName = new FileName("file.txt");

        // Act
        string result = fileName.ToString();

        // Assert
        result.Should().Be("file.txt");
    }
}
