namespace CogniVault.Platform.Core.Abstractions.Persistence.EFCore;

// Abstracted context interface
public interface IDbContext : IDisposable
{
    Task<int> SaveChangesAsync();
    // You can add other common methods if needed
}
