namespace CogniVault.Application.ValueObjects;

// public interface IValueObject<T>
// {
//     T Value { get; set; }
// }

public interface IValueObject<T> : IEquatable<T>, IComparable<T>
{
    // static abstract bool TryParse(T value, out IValueObject<T> result);
    void Validate();
    T Copy();
    string ToString();
    int GetHashCode();
    bool Equals(object obj);
    // bool NotEquals(object obj);
}


public interface IValueObject
{
    void Validate();
    object Copy();
    string ToString();
    int GetHashCode();
    bool Equals(object obj);
}