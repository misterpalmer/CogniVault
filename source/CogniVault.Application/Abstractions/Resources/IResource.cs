using CogniVault.Application.Abstractions.Resources;
using CogniVault.Application.Abstractions.Resources.FileSystem;
using CogniVault.Application.Entities;



namespace CogniVault.Application.Abstractions.Resources;

public interface IResource : IOwnableResource, IResourceActivityProperties, IResourceActivityEvents
{
    ResourceType Type { get; }
    Guid Id { get; }
}