using Microsoft.EntityFrameworkCore;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
}