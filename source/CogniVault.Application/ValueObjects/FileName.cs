namespace CogniVault.Application.ValueObjects;

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
