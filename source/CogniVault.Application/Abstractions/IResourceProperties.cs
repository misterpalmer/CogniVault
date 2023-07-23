namespace CogniVault.Application.Abstractions;

public interface IResourceProperties
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset LastModifiedAt { get; }
}