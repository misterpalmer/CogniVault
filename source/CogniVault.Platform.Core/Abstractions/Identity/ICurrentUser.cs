namespace CogniVault.Platform.Core.Abstractions.Identity;


public interface ICurrentUser<TId> where TId : struct
{
    TId Id { get; }
}

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    string UserName { get; }  
}