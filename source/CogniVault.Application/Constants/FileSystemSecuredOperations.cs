namespace CogniVault.Application.Constants;

[Flags]
public enum FileSystemSecuredOperation
{
    None = 0, // Represents no operations.
    Read = 1 << 0, // Represents a read operation on a resource.
    Write = 1 << 1, // Represents a write operation on a resource.
    Delete = 1 << 2, // Represents a delete operation on a resource.
    ChangePermissions = 1 << 3, // Represents an operation to change the permissions of a resource.
    Move = 1 << 4, // Represents an operation to move a resource.
    Copy = 1 << 5, // Represents an operation to copy a resource.
    Rename = 1 << 6 // Represents an operation to rename a resource.
}

