using CogniVault.Application.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace CogniVault.Application.ValueObjects;

public class DirectoryName : IValueObject<DirectoryName>
{
    private readonly string _value;

    public string Value => _value;

    public DirectoryName(string value)
    {
        _value = value;
        Validate();
    }

    public int CompareTo(DirectoryName? other)
    {
        return _value.CompareTo(other?._value);
    }

    public DirectoryName Copy()
    {
        return new DirectoryName(_value);
    }

    public bool Equals(DirectoryName? other)
    {
        return _value == other?._value;
    }

    public void Validate()
    {
        var validator = new DirectoryNameValidator();
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
        if (obj is DirectoryName other)
        {
            return Equals(other);
        }
        return false;
    }

    public static bool operator ==(DirectoryName left, DirectoryName right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(DirectoryName left, DirectoryName right)
    {
        return !(left == right);
    }
}



// public class DirectoryName : IValueObject<DirectoryName>
// {
//     private readonly string _name;

//     public DirectoryName(string name)
//     {
//         var validator = new DirectoryNameValidator();
//         if (!validator.IsValid(name))
//         {
//             throw new ArgumentException("Invalid directory name", nameof(name));
//         }

//         _name = name;
//     }

//     public int CompareTo(DirectoryName? other)
//     {
//         return other == null ? 1 : _name.CompareTo(other._name);
//     }

//     public DirectoryName Copy()
//     {
//         return new DirectoryName(_name);
//     }

//     public bool Equals(DirectoryName? other)
//     {
//         return other != null && _name == other._name;
//     }

//     public void Validate()
//     {
//         var validator = new DirectoryNameValidator();
//         var validationResult = validator.Validate(_name);
        
//         if (!validationResult.IsValid)
//         {
//             throw new ValidationException(validationResult.Errors);
//         }
//     }

//     public override string ToString()
//     {
//         return _name;
//     }

//     public override int GetHashCode()
//     {
//         return _name.GetHashCode();
//     }

//     public override bool Equals(object obj)
//     {
//         return Equals(obj as DirectoryName);
//     }

//     public static bool operator ==(DirectoryName left, DirectoryName right)
//     {
//         return Equals(left, right);
//     }

//     public static bool operator !=(DirectoryName left, DirectoryName right)
//     {
//         return !Equals(left, right);
//     }
// }