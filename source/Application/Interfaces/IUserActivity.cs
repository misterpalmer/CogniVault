namespace CogniVault.Application.Interfaces;

public interface IUserActivity
{
    DateTime LastLoginAt { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    void ModifyLastLoginAt(DateTime lastLoginAt);
    void ModifyUpdatedAt(DateTime updatedAt);
}