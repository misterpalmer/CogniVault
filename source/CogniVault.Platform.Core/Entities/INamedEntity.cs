namespace CogniVault.Platform.Core.Entities;

public interface INamedEntity<TId> : IIdentityEntity<TId>
{
    IValueObject Name { get; set; }
}