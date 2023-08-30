using CogniVault.Platform.Core.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Platform.Identity.ValueObjects;

public class OrganizationName : IValueObject<OrganizationName>
{
    public string Value { get; private set; }

    private OrganizationName(string value)
    {
        Value = value;
    }

    public int CompareTo(OrganizationName? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public OrganizationName Copy()
    {
        return new OrganizationName(Value);
    }

    public bool Equals(OrganizationName? other)
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

    public static async Task<OrganizationName> CreateAsync(string value, IValidator<OrganizationName> validator)
    {
        var instance = new OrganizationName(value);
        var results = await validator.ValidateAsync(instance);
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
        return instance;
    }

    public static OrganizationName Null => new OrganizationName(string.Empty);

    public static explicit operator OrganizationName(string? v)
    {
        if (v == null)
        {
            throw new ArgumentNullException(nameof(v), "The input string cannot be null.");
        }

        return new OrganizationName(v);
    }
}