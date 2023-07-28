using CogniVault.Application.Validators;

namespace CogniVault.Application.Tests.Validators;

public class GroupNameValidatorTests
{
    private IValidator<string> validator;

    public GroupNameValidatorTests()
    {
        validator = new GroupNameValidator();
    }

    [Theory]
    [InlineData("Admins", true)]
    [InlineData("Users", true)]
    [InlineData("Developers", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("InvalidName!", false)] // Assume special characters are not allowed
    public void IsValid_Returns_Expected_Results(string groupName, bool isValid)
    {
        // Act
        var result = validator.IsValid(groupName);

        // Assert
        result.Should().Be(isValid);
    }
}
