using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;


public class OrganizationConfiguration : IEntityTypeConfiguration<PlatformOrganization>
{
    public void Configure(EntityTypeBuilder<PlatformOrganization> builder)
    {
        // Set table name if needed
        builder.ToTable("Organizations");

        // Configure Id as a primary key
        builder.HasKey(o => o.Id);

        // Configure relationships
        builder.HasMany(o => o.Tenants)
               .WithOne(t => t.Organization) // Replace with navigation property in PlatformTenant if exists
               .HasForeignKey(t => t.OrganizationId); // Replace with actual foreign key if different

        // Configure properties for Organization
        // builder.Property(o => o.LogoUri)
        //        .HasColumnType("text") // Use appropriate data type
        //        .IsRequired(false);
        builder.Ignore(o => o.LogoUri);

        builder.Property(o => o.IsEnabled)
               .IsRequired();

        // Configure the OrganizationName value object
        builder.OwnsOne(o => o.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("OrganizationName")
                .IsRequired()
                .HasMaxLength(255);
        });

        // Exclude JSON-ignored properties from the database model
        builder.Ignore(e => e.IsNullObject);

        // builder.OwnsOne(o => o.Name, name =>
        // {
        //     name.Property(n => n.Value)
        //         .HasColumnName("Name")
        //         .IsRequired()
        //         .HasMaxLength(255)
        //         .HasConversion(
        //             v => v, // Convert OrganizationName to string
        //             v => new OrganizationName(v) // Convert string to OrganizationName
        //         );
        // });
    }
}