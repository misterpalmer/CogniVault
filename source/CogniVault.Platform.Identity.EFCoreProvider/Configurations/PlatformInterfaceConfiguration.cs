using CogniVault.Platform.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;

public class PlatformInterfaceConfiguration : IEntityTypeConfiguration<PlatformInterface>
{
    public void Configure(EntityTypeBuilder<PlatformInterface> builder)
    {
        // Set table name if needed
        builder.ToTable("Interfaces");

        // Configure Id as a primary key if needed
        builder.HasKey(e => e.Id);

        // Configure relationships
        builder.HasOne(e => e.Tenant) // Assuming PlatformTenant is another entity
              .WithMany(i => i.Interfaces) // Replace with navigation property if available
              .HasForeignKey(t => t.TenantId) // Replace with actual FK column name if different
              .IsRequired(false); // Makes it optional

        // Configure properties
        builder.Property(e => e.InterfaceIdentifier)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(e => e.ConnectionString)
               .IsRequired()
               .HasMaxLength(1024);

        builder.Property(e => e.AdminEmail)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(e => e.LogoUri)
               .HasColumnType("text") // Replace with appropriate data type
               .IsRequired(false);

        builder.Property(e => e.IsEnabled)
               .IsRequired();

        builder.Property(e => e.IsDefault)
               .IsRequired();

        builder.Property(e => e.DisabledOnUtc)
               .IsRequired();

        builder.OwnsOne(o => o.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("InterfaceName")
                .IsRequired()
                .HasMaxLength(255);
        });

        // Exclude JSON-ignored properties from the database model
        builder.Ignore(e => e.IsNullObject);
    }
}
