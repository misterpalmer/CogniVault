namespace CogniVault.Application.Abstractions.Resources.AccessControl.Users;

public interface IFileSystemUserFactory
{
    IFileSystemUser CreateUser(string username, string fullName, string email);
    IFileSystemUser CreateSuperUser(string username, string fullName, string email);
}