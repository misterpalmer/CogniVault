using CogniVault.Application.Validators;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Tests.Validators;

// public class PasswordValidatorTests
// {
//     private PasswordValidator validator;

//     public PasswordValidatorTests()
//     {
//         validator = new PasswordValidator();
//     }

//     [Theory]
//     [InlineData("Password1!", true)]
//     [InlineData("Password1!Password1!", true)]
//     [InlineData("Password1", false)] // Missing special character
//     [InlineData("Password!", false)] // Missing digit
//     [InlineData("password1!", false)] // Missing uppercase letter
//     [InlineData("PASSWORD1!", false)] // Missing lowercase letter
//     [InlineData("Pass1!", false)] // Too short
//     public void Validate_Returns_ExpectedResults(string password, bool expectedIsValid)
//     {
//         // Act
//         var isValid = validator.IsValid(new Password(password));

//         // Assert
//         isValid.Should().Be(expectedIsValid);
//     }

//     // [Theory]
//     // [InlineData("Password1!", false)]
//     // [InlineData("Password1!Password1!", false)]
//     // [InlineData("Password1", true)] // Missing special character
//     // [InlineData("Password!", true)] // Missing digit
//     // [InlineData("password1!", true)] // Missing uppercase letter
//     // [InlineData("PASSWORD1!", true)] // Missing lowercase letter
//     // [InlineData("Pass1!", true)] // Too short
//     // public void Validate_Returns_ExpectedResultsForIsNotValid(Password password, bool expectedIsNotValid)
//     // {
//     //     // Act
//     //     var isValid = validator.IsNotValid(password);
//     //     var isNotValid = !isValid;

//     //     // Assert
//     //     isNotValid.Should().Be(expectedIsNotValid);
//     // }
// }
