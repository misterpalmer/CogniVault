using CogniVault.Application.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class FileName : IValueObject<FileName>
{
    private readonly string _value;

    public string Value => _value;

    public FileName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(FileName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public FileName Copy()
    {
        return new FileName(_value);
    }

    public bool Equals(FileName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new FileNameValidator();
        ValidationResult results = validator.Validate(this);
        
        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);
        }
    }

    public override string ToString()
    {
        return _value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is FileName other)
        {
            return Equals(other);
        }
        return false;
    }
}

// public class ResourceName : IEquatable<ResourceName>
// {
//     public string Value { get; }

//     public ResourceName(string value)
//     {
//         Value = value;
//     }

//     public bool Equals(ResourceName other)
//     {
//         if (ReferenceEquals(null, other)) return false;
//         if (ReferenceEquals(this, other)) return true;
//         return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
//     }

//     public override bool Equals(object obj)
//     {
//         if (ReferenceEquals(null, obj)) return false;
//         if (ReferenceEquals(this, obj)) return true;
//         if (obj.GetType() != GetType()) return false;
//         return Equals((ResourceName)obj);
//     }

//     public override int GetHashCode()
//     {
//         return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
//     }

//     public override string ToString()
//     {
//         return Value;
//     }

//     public static bool operator ==(ResourceName left, ResourceName right)
//     {
//         if (ReferenceEquals(null, left) && ReferenceEquals(null, right))
//             return true;
//         if (ReferenceEquals(null, left) || ReferenceEquals(null, right))
//             return false;
//         return left.Equals(right);
//     }

//     public static bool operator !=(ResourceName left, ResourceName right)
//     {
//         return !(left == right);
//     }
// }
