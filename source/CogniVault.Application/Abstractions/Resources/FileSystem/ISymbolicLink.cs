namespace CogniVault.Application.Abstractions.Resources.FileSystem.Symlink;

public interface ISymbolicLink : IResource
{
    IResource Target { get; }
    void SetTarget(IResource target);
}