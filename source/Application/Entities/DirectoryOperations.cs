using CogniVault.Application.Interfaces;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Entities;

public class DirectoryOperations : IResourceOperation
{
    public void Create(ResourceName name, IUser user)
    {
        throw new NotImplementedException();
    }

    public void Delete(ResourceName name)
    {
        throw new NotImplementedException();
    }

    public IResource Get(ResourceName name)
    {
        throw new NotImplementedException();
    }

    public void Update(ResourceName name, IResource newEntity)
    {
        throw new NotImplementedException();
    }
}