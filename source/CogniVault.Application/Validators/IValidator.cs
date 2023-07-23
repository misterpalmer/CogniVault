namespace CogniVault.Application.Validators;

public interface IValidator<T>
{
    bool IsValid(T value);
}