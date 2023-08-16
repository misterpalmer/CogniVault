namespace CogniVault.Application.Events;

public class FileSystemResourceCreatedEvent
{
    
}


// ResourceRenamed: Triggered when a resource (file or directory) is renamed.
// ResourceMetadataChanged: Triggered when the metadata of a resource is changed, such as file attributes or timestamps.
// UserCreated: Triggered when a new user is added to the system.
// UserDeleted: Triggered when a user is removed from the system.
// GroupCreated: Triggered when a new user group is created.
// GroupDeleted: Triggered when a user group is deleted.
// UserAddedToGroup: Triggered when a user is added to a group.
// UserRemovedFromGroup: Triggered when a user is removed from a group.
// FileSystemLoaded: Triggered when the file system is fully loaded and ready to be used.
// FileSystemClosed: Triggered when the file system is properly closed, perhaps prior to an application shutdown.
// ResourceCreated: Triggered when a new resource is created (file or directory).
// ResourceDeleted: Triggered when a resource is deleted.
// ResourceMoved: Triggered when a resource is moved from one location to another.
// ResourceCopied: Triggered when a resource is copied.
// ResourceRead: Triggered when a resource is read.
// ResourceWritten: Triggered when data is written to a resource.
// ResourceLocked: Triggered when a lock is acquired on a resource.
// ResourceUnlocked: Triggered when a lock is released on a resource.
// PermissionGranted: Triggered when a permission is granted to a user or a group.
// PermissionRevoked: Triggered when a permission is revoked from a user or a group.

// PermissionChanged: Triggered when permissions for a resource or a user are modified.
// QuotaExceeded: Triggered when a user or group exceeds their assigned storage quota.
// QuotaChanged: Triggered when the storage quota for a user or group is changed.
// FileSystemResized: Triggered when the overall size of the file system is increased or decreased.
// FileSystemRecoveryStarted: Triggered when recovery procedures start, such as after a crash.
// FileSystemRecoveryCompleted: Triggered when recovery procedures complete successfully.
// FileSystemCleanupStarted: Triggered when a cleanup operation (like deletion of temporary or unused files) starts.
// FileSystemCleanupCompleted: Triggered when a cleanup operation completes.
// FileSystemBackupStarted: Triggered when a backup operation starts.
// FileSystemBackupCompleted: Triggered when a backup operation completes successfully.