namespace CogniVault.Application.Abstractions;
public interface IResourceDeleteOperation : IResourceOperation
{
    IResource Delete();
}