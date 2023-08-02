using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

using FluentValidation;

namespace CogniVault.Application.Tests.ValueTypes;


public class UsernameTests
{
    private UsernameValidator _validator = new UsernameValidator();

    [Theory]
    [InlineData("username1", true)]
    [InlineData("USERNAME1", true)]
    [InlineData("user", true)]
    [InlineData("u", false)] // too short
    [InlineData(null, false)] // null value
    [InlineData("", false)] // empty string
    [InlineData("user_name", true)] // includes underscore
    [InlineData("user name", false)] // includes space
    [InlineData("user@name", false)] // includes special character
    public void Username_IsValid_WhenConstructed(string value, bool expectedIsValid)
    {
        // Act
        if (expectedIsValid)
        {
            var username = new Username(value);
            // Assert
            Assert.Equal(value, username.Value);
        }
        else
        {
            // Assert
            Assert.Throws<ValidationException>(() => new Username(value));
        }
    }

    [Theory]
    [InlineData("username1", "username1", true)]
    [InlineData("username1", "USERNAME1", false)] // case-sensitive comparison
    [InlineData("username1", "username2", false)]
    public void Username_Equals_ReturnsExpectedResult(string value1, string value2, bool expectedEquals)
    {
        // Arrange
        var username1 = new Username(value1);
        var username2 = new Username(value2);

        // Act
        var result = username1.Equals(username2);

        // Assert
        Assert.Equal(expectedEquals, result);
    }
    
    [Fact]
    public void Username_CopiesCorrectly()
    {
        // Arrange
        var originalUsername = new Username("username1");

        // Act
        var copiedUsername = originalUsername.Copy();

        // Assert
        Assert.Equal(originalUsername.Value, copiedUsername.Value);
        Assert.NotSame(originalUsername, copiedUsername);
    }
}

