namespace CogniVault.Platform.Core.Entities;

public abstract class EntityBase<TId> : IEntityBase<TId>, IAuditableEntity where TId : IEquatable<TId>
{
    public TId Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public string ModifiedBy { get; set; }
    public DateTimeOffset ModifiedOnUtc { get; set; }
    private int? _requestedHashCode;

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is EntityBase<TId>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (EntityBase<TId>) obj;

        if (item.IsTransient() || IsTransient())
            return false;

        return item.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode =
                    Id.GetHashCode() ^
                    31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }

        return base.GetHashCode();
    }

    public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        return left.Equals(right);
    }

    public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
    {
        return !(left == right);
    }

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }
}