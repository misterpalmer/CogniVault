namespace CogniVault.Application.Constants;

public enum FileSystemSecuredOperation
{
    // Represents a read operation on a resource.
    Read,

    // Represents a write operation on a resource.
    Write,

    // Represents a delete operation on a resource.
    Delete,

    // Represents an operation to change the permissions of a resource.
    ChangePermissions,

    // Represents an operation to move a resource.
    Move,

    // Represents an operation to copy a resource.
    Copy,

    // Represents an operation to rename a resource.
    Rename
}
