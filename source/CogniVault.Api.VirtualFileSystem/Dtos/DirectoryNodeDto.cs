namespace CogniVault.Api.VirtualFileSystem.Dtos;

public class DirectoryNodeDto
{
    public Guid PlatformInterfaceId { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; }
}