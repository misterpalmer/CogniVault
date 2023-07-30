using CogniVault.Application.Events;
using CogniVault.Application.ValueObjects;

namespace CogniVault.Application.Abstractions;

public interface IResourceName : IComparable<IResourceName>
{
    // This property gets or sets the name of the resource.
    // The set accessor should validate the new name using the NameValidator
    // and trigger the Renamed event if the name is changed.
    IResourceName Name { get; set; }
    // This event is triggered when the resource is renamed.
    event EventHandler<NameChangedEventArgs> Renamed;
    IResourceName RenameTo(string name, bool overwrite);
}