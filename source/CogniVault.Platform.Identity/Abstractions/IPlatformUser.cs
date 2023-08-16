namespace CogniVault.Platform.Identity.Abstractions;

public interface IPlatformUser<TId> where TId : struct
{
    Guid Id { get; set; }
}