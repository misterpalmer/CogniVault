namespace CogniVault.Application.Abstractions;

public interface IResourceOperations : IResourceAccess, IResourceMoveOperation, IResourceCopyOperation, IResourceCreateOperation, IResourceDeleteOperation, IResourceTargetDirectory
{
}
