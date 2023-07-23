namespace CogniVault.Application.Abstractions;

public interface IResourceOperation
{
    IResource Execute(IResource target, bool overwrite);
}