namespace CogniVault.Application.Abstractions;

public interface IResourceCreateOperation : IResourceOperation
{
    IResource Create();
    IResource Create(bool createParent);
}