// using CogniVault.Application.Interfaces;
// using CogniVault.Application.Validators;
// using FluentValidation;
// using FluentValidation.Results;

// namespace CogniVault.Application.ValueObjects;

// public class Username : IValueObject<Username>
// {
//     private readonly string _value;

//     public string Value => _value;

//     public Username(string value)
//     {
//         _value = value;
//         Validate();
//     }

//     public int CompareTo(Username? other)
//     {
//         return _value.CompareTo(other?._value);
//     }

//     public Username Copy()
//     {
//         return new Username(_value);
//     }

//     public bool Equals(Username? other)
//     {
//         return _value == other?._value;
//     }

//     public void Validate()
//     {
//         var validator = new UsernameValidator();
//         ValidationResult results = validator.Validate(this);

//         if (!results.IsValid)
//         {
//             throw new ValidationException(results.Errors);
//         }
//     }

//     public override string ToString()
//     {
//         return _value;
//     }

//     public override int GetHashCode()
//     {
//         return _value.GetHashCode();
//     }

//     public override bool Equals(object obj)
//     {
//         if (obj is Username other)
//         {
//             return Equals(other);
//         }
//         return false;
//     }
// }