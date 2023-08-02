namespace CogniVault.Application.Abstractions.Resources.FileSystem.Files;

public interface IResourceContent
{
    string DefaultContentName { get; }
    IEnumerable<string> GetContentNames();
    IResourceContent GetContent();
    IResourceContent GetContent(string contentName);
}