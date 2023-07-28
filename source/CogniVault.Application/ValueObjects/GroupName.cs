using CogniVault.Application.Interfaces;
using CogniVault.Application.Validators;

namespace CogniVault.Application.ValueObjects;

public class GroupName : IEquatable<GroupName>
{
    public string Value { get; }

    public GroupName(string value, IValidator<string> validator)
    {
        if (!validator.IsValid(value))
        {
            throw new ArgumentException("Invalid group name");
        }

        Value = value;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as GroupName);
    }

    public bool Equals(GroupName other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }

    public static bool operator ==(GroupName left, GroupName right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(GroupName left, GroupName right)
    {
        return !(left == right);
    }
}
