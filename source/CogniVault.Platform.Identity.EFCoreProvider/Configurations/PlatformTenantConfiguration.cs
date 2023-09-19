using CogniVault.Platform.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;

public class PlatformTenantConfiguration : IEntityTypeConfiguration<PlatformTenant>
{
    public void Configure(EntityTypeBuilder<PlatformTenant> builder)
    {
        // Configure table name
        builder.ToTable("Tenants");

        // Configure primary key
        builder.HasKey(t => t.Id);

        // Configure relationships
        builder.HasOne(t => t.Organization)
               .WithMany("Tenants") // Replace with navigation property in PlatformOrganization if it exists
               .HasForeignKey("OrganizationId"); // Replace with actual foreign key if different

        builder.HasMany(i => i.Interfaces)
               .WithOne(t => t.Tenant) // Replace with navigation property in PlatformInterface if exists
               .HasForeignKey(i => i.TenantId); // Replace with actual foreign key if different

        // Configure properties
        builder.Property(t => t.TenantIdentifier)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(t => t.ConnectionString)
               .IsRequired(false);

        builder.Property(t => t.AdminEmail)
               .IsRequired(false);

        builder.Property(t => t.LogoUri)
               .HasColumnType("text")
               .IsRequired(false);

        builder.Property(t => t.IsEnabled)
               .IsRequired();

        builder.Property(t => t.IsDefault)
               .IsRequired();

        builder.Property(t => t.DisabledOnUtc)
               .IsRequired();

        // Configure TenantName value object
        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(255);
            // Conversion can be added here if TenantName has a non-public constructor or special instantiation needs
        });

        // Exclude JSON-ignored properties from the database model
        builder.Ignore(e => e.IsNullObject);
    }
}
