using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Interfaces;

public interface IResourceOperation
{
    void Create(ResourceName resourceName, IUser user);
    IResource Get(ResourceName resourceName);
    void Delete(ResourceName resourceName);
    void Update(ResourceName resourceName, IResource newEntity);
}