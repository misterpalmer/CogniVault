using MediatR;

namespace CogniVault.Platform.Core.Entities;

public abstract class DomainEntityBase : EntityBase<Guid>, IDomainEntity
{
    private List<INotification> _domainEvents = new();

    private int? _requestedHashCode;

    public IReadOnlyCollection<INotification> GetDomainEvents()
    {
        return (IReadOnlyCollection<INotification>) _domainEvents?.AsReadOnly()! ?? new List<INotification>();
    }

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is DomainEntityBase))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (DomainEntityBase) obj;

        if (item.IsTransient() || IsTransient())
            return false;
        return item.Id == Id;
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

    public static bool operator ==(DomainEntityBase left, DomainEntityBase right)
    {
        if (Equals(left, null))
            return Equals(right, null) ? true : false;
        return left.Equals(right);
    }

    public static bool operator !=(DomainEntityBase left, DomainEntityBase right)
    {
        return !(left == right);
    }
}