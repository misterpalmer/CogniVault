namespace CogniVault.Application.ValueObjects;

public interface IValueObject<T>
{
    T Value { get; set; }
}