using MediatR;

namespace CogniVault.Platform.Core.Entities;


public interface IDomainEntity
{
    IReadOnlyCollection<INotification> GetDomainEvents();
    void AddDomainEvent(INotification eventItem);
    void RemoveDomainEvent(INotification eventItem);
    void ClearDomainEvents();
}
