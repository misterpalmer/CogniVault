namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IUserActivity
{
    DateTimeOffset LastLoginAt { get; }
    void ModifyLastLoginAt(DateTimeOffset lastLoginAt);
    void ModifyUpdatedAt(DateTimeOffset updatedAt);
}