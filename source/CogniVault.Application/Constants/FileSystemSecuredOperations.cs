namespace CogniVault.Application.Constants;

public enum FileSystemSecuredOperation
{
    Read, // Represents a read operation on a resource.
    Write, // Represents a write operation on a resource.
    Delete, // Represents a delete operation on a resource.
    ChangePermissions, // Represents an operation to change the permissions of a resource.
    Move, // Represents an operation to move a resource.
    Copy, // Represents an operation to copy a resource.
    Rename // Represents an operation to rename a resource.
}
