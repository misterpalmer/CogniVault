using CogniVault.Application.VirtualFileSystem.Entities;

namespace CogniVault.Application.VirtualFileSystem.Provider.Specifications;

public class GetAllFromDirectoryNodeSpecification : GetAllFromNodeSpecification<DirectoryNode>
{
    public GetAllFromDirectoryNodeSpecification(Guid parentId) : base(parentId) { }
}