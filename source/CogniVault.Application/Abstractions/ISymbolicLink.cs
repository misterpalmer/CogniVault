namespace CogniVault.Application.Abstractions;

public interface ISymbolicLink : IResource
{
    IResource Target { get; }
    void SetTarget(IResource target);
}