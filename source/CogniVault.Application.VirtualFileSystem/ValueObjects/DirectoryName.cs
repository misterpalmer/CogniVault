using CogniVault.Platform.Core.Entities;

using FluentValidation;

namespace CogniVault.Application.VirtualFileSystem.ValueObjects;

public class DirectoryName : IValueObject<DirectoryName>
{
    public string Value { get; }

    private DirectoryName(string value)
    {
        Value = value;
    }

    public static DirectoryName Null => new DirectoryName(string.Empty);

    public static DirectoryName RootNode => new DirectoryName("/");

    public static async Task<DirectoryName> CreateAsync(string value, IValidator<DirectoryName> validator)
    {
        var instance = new DirectoryName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public int CompareTo(DirectoryName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public DirectoryName Copy()
    {
        return new DirectoryName(Value);
    }

    public bool Equals(DirectoryName? other)
    {
        return Value == other?.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public static explicit operator DirectoryName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new DirectoryName(v);
    }
}