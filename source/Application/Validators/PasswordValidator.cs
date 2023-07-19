using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Validators;

public class PasswordValidator : IPasswordValidator
{
    public bool Validate(string password)
    {
        // Check if the password meets the required criteria
        if (password.Length < 8)
        {
            return false;
        }

        // Check for at least one uppercase letter
        if (!password.Any(char.IsUpper))
        {
            return false;
        }

        // Check for at least one lowercase letter
        if (!password.Any(char.IsLower))
        {
            return false;
        }

        // Check for at least one digit
        if (!password.Any(char.IsDigit))
        {
            return false;
        }

        // Check for at least one special character
        if (!password.Any(IsSpecialCharacter))
        {
            return false;
        }

        return true;
    }

    private bool IsSpecialCharacter(char c)
    {
        // Define your own set of special characters
        string specialCharacters = "!@#$%^&*()_+{}[]|\\:;'<>?,./\"";
        return specialCharacters.Contains(c);
    }
}