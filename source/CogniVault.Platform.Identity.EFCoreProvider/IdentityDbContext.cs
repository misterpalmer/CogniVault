using CogniVault.Platform.Identity.Entities;

using Microsoft.EntityFrameworkCore;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        base.OnConfiguring(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        modelBuilder.Entity<PlatformOrganization>()
        .HasMany(o => o.Tenants)
        .WithOne(t => t.Organization)
        .HasForeignKey(t => t.OrganizationId);

        modelBuilder.Entity<PlatformTenant>()
            .HasMany(t => t.Interfaces)
            .WithOne(i => i.Tenant)
            .HasForeignKey(i => i.TenantId);
    }
}