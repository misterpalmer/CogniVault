namespace CogniVault.Application.Abstractions;

public interface IResourceTargetDirectory
{
    IDirectory OperationTargetDirectory { get; }
}