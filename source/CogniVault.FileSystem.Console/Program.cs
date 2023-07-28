using CogniVault.Application.Entities;


// Accessing the singleton instance
var fileSystem = FileSystem.Instance;

// Define a path
string path = "/path/to/resource";

// Get the resource type
var resourceType = fileSystem.GetResourceType(path);
Console.WriteLine($"Resource type of {path}: {resourceType}");

// Check if the resource exists
bool exists = fileSystem.ResourceExists(path, resourceType);
Console.WriteLine($"Does the resource exist? {exists}");

// Dispose the file system when done
fileSystem.Dispose();
