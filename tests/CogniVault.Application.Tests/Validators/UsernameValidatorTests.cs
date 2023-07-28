using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Tests.Validators;


public class UsernameValidatorTests
{
    private IUsernameValidator validator;

    public UsernameValidatorTests()
    {
        validator = new UsernameValidator();
    }

    [Theory]
    [InlineData("user", true)]
    [InlineData("username123", true)]
    [InlineData("username_123", true)]
    [InlineData("us", false)] // Too short
    [InlineData("username123username123", false)] // Too long
    [InlineData("", false)] // Empty string
    [InlineData(null, false)] // Null string
    [InlineData(" ", false)] // Whitespace
    [InlineData("username!", false)] // Invalid character
    public void Validate_Returns_ExpectedResults(string username, bool expectedIsValid)
    {
        // Act
        var isValid = validator.Validate(username);

        // Assert
        isValid.Should().Be(expectedIsValid);
    }
}
