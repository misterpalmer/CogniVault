namespace CogniVault.Application.Interfaces;

public interface IPasswordValidator
{
    bool Validate(string password);
}