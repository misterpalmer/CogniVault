namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface ISuperUser : IAccessControlEntity
{
    void PerformSuperUserAction();
}
