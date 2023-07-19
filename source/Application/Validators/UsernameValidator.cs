using System.Text.RegularExpressions;

using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Validators;

public class UsernameValidator : IUsernameValidator
{
    public bool Validate(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return false;
        }

        // Add additional validation rules as per your requirements
        if (username.Length < 3 || username.Length > 20)
        {
            return false;
        }

        // Ensure the username contains only alphanumeric characters and underscores
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
        {
            return false;
        }

        return true;
    }
}