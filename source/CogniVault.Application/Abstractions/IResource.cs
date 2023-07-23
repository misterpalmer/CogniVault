using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;


namespace CogniVault.Application.Abstractions;

public interface IResource : INamedResource, IResourceAddress, IResourceProperties, IResourceActivity
{
    IUser Owner { get; set; }
    void SetOwner(IUser newOwner);
    bool IsHidden { get; set; }
}