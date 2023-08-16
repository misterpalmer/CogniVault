using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IUserManagement
{
    Quota Quota { get; }
    void ChangeQuota(Quota newQuota);
}