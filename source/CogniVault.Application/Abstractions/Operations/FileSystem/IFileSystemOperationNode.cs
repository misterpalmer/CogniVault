namespace CogniVault.Application.Abstractions.Operations.FileSystem;

public interface IFileSystemOperationNode
{
    Guid Id { get; set; }
    IFileSystemOperation Operation { get; set; }
    List<IFileSystemOperationNode> Children { get; set; }
    Task<bool> CanPerformOperationAsync(IFileSystemOperation operation);
}