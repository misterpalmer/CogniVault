namespace CogniVault.Platform.Core.Entities;

public interface IValueObject<T> : IEquatable<T>, IComparable<T>
{
    // static abstract bool TryParse(T value, out IValueObject<T> result);
    T Copy();
}


public interface IValueObject
{
    object Copy();
}