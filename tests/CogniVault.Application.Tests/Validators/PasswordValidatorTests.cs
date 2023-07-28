using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Tests.Validators;

public class PasswordValidatorTests
{
    private IValidator<string> validator;

    public PasswordValidatorTests()
    {
        validator = new PasswordValidator();
    }

    [Theory]
    [InlineData("Password1!", true)]
    [InlineData("Password1!Password1!", true)]
    [InlineData("Password1", false)] // Missing special character
    [InlineData("Password!", false)] // Missing digit
    [InlineData("password1!", false)] // Missing uppercase letter
    [InlineData("PASSWORD1!", false)] // Missing lowercase letter
    [InlineData("Pass1!", false)] // Too short
    public void Validate_Returns_ExpectedResults(string password, bool expectedIsValid)
    {
        // Act
        var isValid = validator.IsValid(password);

        // Assert
        isValid.Should().Be(expectedIsValid);
    }
}
