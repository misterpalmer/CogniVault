// using CogniVault.Application.Abstractions.Resources.AccessControl.Users;
// using CogniVault.Application.Interfaces;
// using CogniVault.Application.Validators;
// using CogniVault.Platform.Identity.Abstractions;

// using FluentValidation;
// using FluentValidation.Results;

// namespace CogniVault.Application.ValueObjects;

// public class Password : IValueObject<Password>
// {
//     private readonly IPasswordEncryptor encryptor;
//     public string Value { get; }

//     public Password(string value, IPasswordEncryptor encryptor)
//     {
//         this.encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));
//         this.Value = value;

//         Validate();
//     }

//     public bool Verify(string passwordToVerify)
//     {
//         return encryptor.Verify(passwordToVerify, Value);
//     }

//     public int CompareTo(Password? other)
//     {
//         return Value.CompareTo(other?.Value);
//     }

//     public Password Copy()
//     {
//         return new Password(Value, encryptor);
//     }

//     public bool Equals(Password? other)
//     {
//         if (other == null)
//         {
//             return false;
//         }

//         return Value == other.Value;
//     }

//     public override bool Equals(object obj)
//     {
//         if (obj is Password password)
//         {
//             return Value.Equals(password.Value);
//         }

//         return false;
//     }

//     public override int GetHashCode()
//     {
//         return Value.GetHashCode();
//     }

//     public override string ToString()
//     {
//         return Value;
//     }

//     public void Validate()
//     {
//         var validator = new PasswordValidator();
//         ValidationResult results = validator.Validate(this);
        
//         if (!results.IsValid)
//         {
//             throw new ValidationException(results.Errors);
//         }
//     }
// }
