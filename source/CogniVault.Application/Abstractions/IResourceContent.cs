namespace CogniVault.Application.Abstractions;

public interface IResourceContent
{
    string DefaultContentName { get; }
    IEnumerable<string> GetContentNames();
    IResourceContent GetContent();
    IResourceContent GetContent(string contentName);
}