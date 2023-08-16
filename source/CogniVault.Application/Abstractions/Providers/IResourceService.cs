namespace CogniVault.Application.Abstractions;

public interface IResourceService
{
    IResourceService GetService();
    T GetService<T>() where T : IResourceService;
}