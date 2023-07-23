namespace CogniVault.Application.Abstractions;

public interface IResourceService
{
    IService GetService();
    T GetService<T>() where T : IService;
}