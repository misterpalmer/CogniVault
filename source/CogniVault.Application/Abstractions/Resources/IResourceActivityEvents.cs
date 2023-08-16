namespace CogniVault.Application.Abstractions;  


public delegate void ResourceActivityEventHandler(object sender, ResourceActivityEventArgs e);

public class ResourceActivityEventArgs : EventArgs
{
    // Define any properties or methods you need for the event arguments.
}

public interface IResourceActivityEvents
{
    event ResourceActivityEventHandler Created;
    event ResourceActivityEventHandler Deleted;
    event ResourceActivityEventHandler Changed;
    event ResourceActivityEventHandler Activity;
    bool SupportsActivityEvents { get; }
}
