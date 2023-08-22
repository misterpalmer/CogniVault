using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.Tests.ValueObjects;


public class GroupNameTests
{
    private GroupNameValidator _validator;

    public GroupNameTests()
    {
        _validator = new GroupNameValidator();
    }

    [Theory]
    [InlineData("ValidGroupName")]
    [InlineData("AnotherValidGroupName")]
    public void GroupName_WithValidValue_ShouldSetCorrectValue(string value)
    {
        // Arrange

        // Act
        var groupName = new GroupName(value);

        // Assert
        groupName.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GroupName_WithInvalidValue_ShouldThrowValidationException(string value)
    {
        // Arrange

        // Act
        Action act = () => new GroupName(value);

        // Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact]
    public void GroupName_Equals_ShouldReturnTrueForEqualGroupNames()
    {
        // Arrange
        var groupName1 = new GroupName("ValidGroupName");
        var groupName2 = new GroupName("ValidGroupName");

        // Act
        bool result = groupName1.Equals(groupName2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GroupName_Equals_ShouldReturnFalseForDifferentGroupNames()
    {
        // Arrange
        var groupName1 = new GroupName("ValidGroupName");
        var groupName2 = new GroupName("AnotherValidGroupName");

        // Act
        bool result = groupName1.Equals(groupName2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GroupName_ToString_ShouldReturnCorrectValue()
    {
        // Arrange
        var groupName = new GroupName("ValidGroupName");

        // Act
        string result = groupName.ToString();

        // Assert
        result.Should().Be("ValidGroupName");
    }
}


