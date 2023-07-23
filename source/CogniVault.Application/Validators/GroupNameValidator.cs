using CogniVault.Application.Interfaces;

namespace CogniVault.Application.Validators;

public class GroupNameValidator : IGroupNameValidator
{
    public bool Validate(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
        {
            return false; // Group name cannot be null, empty, or contain only whitespace
        }

        if (groupName.Length < 3 || groupName.Length > 50)
        {
            return false; // Group name should be between 3 and 50 characters long
        }

        if (!groupName.All(c => char.IsLetterOrDigit(c) || c == ' '))
        {
            return false; // Group name can only contain letters, digits, and spaces
        }

        return true; // Group name is valid
    }
}