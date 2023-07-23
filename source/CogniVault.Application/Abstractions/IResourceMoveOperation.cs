namespace CogniVault.Application.Abstractions;

public interface IResourceMoveOperation : IResourceOperation
{
    IResource MoveTo(IResource target, bool overwrite);
    IResource MoveToDirectory(IResource target, bool overwrite);
}