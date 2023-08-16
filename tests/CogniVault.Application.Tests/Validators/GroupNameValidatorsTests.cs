using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.Validators;

public class GroupNameValidatorTests
{
    private GroupNameValidator validator;

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
        var result = validator.IsValid(new GroupName(groupName));

        // Assert
        result.Should().Be(isValid);
    }
}
