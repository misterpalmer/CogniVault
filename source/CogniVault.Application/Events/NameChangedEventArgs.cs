using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Events;

// This class represents the arguments for the Renamed event.
public class NameChangedEventArgs : EventArgs
{
    public ResourceName OldName { get; }
    public ResourceName NewName { get; }

    public NameChangedEventArgs(ResourceName oldName, ResourceName newName)
    {
        OldName = oldName;
        NewName = newName;
    }
}