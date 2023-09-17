using CogniVault.Platform.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CogniVault.Platform.Identity.EFCore.Configurations;
public class PlatformUserConfiguration : IEntityTypeConfiguration<PlatformUser>
{
    public void Configure(EntityTypeBuilder<PlatformUser> builder)
    {
        // Configure Id as a primary key
        builder.HasKey(e => e.Id);

        // Configurations for owned types
        builder.OwnsOne(o => o.Username, username =>
        {
            username.Property(u => u.Value)
                    .HasColumnName("Username")
                    .IsRequired()
                    .HasMaxLength(255);
        });

        builder.OwnsOne(o => o.Password, password =>
        {
            password.Property(p => p.Value)
                    .HasColumnName("Password")
                    .IsRequired()
                    .HasMaxLength(255);
        });

        builder.OwnsOne(o => o.Email, email =>
        {
            email.Property(e => e.Value)
                 .HasColumnName("Email")
                 .IsRequired()
                 .HasMaxLength(255);
        });

        builder.OwnsOne(o => o.Quota, quota =>
        {
            quota.Property(q => q.Value)
                 .HasColumnName("Quota")
                 .IsRequired();
        });

        
        builder.Property(e => e.TimeZone)
               .HasConversion(v => v.Id, v => TimeZoneInfo.FindSystemTimeZoneById(v))
               .IsRequired();
        
        builder.Property(e => e.CreatedAt)
               .IsRequired();

        builder.Property(e => e.UpdatedAt)
               .IsRequired();

        builder.Property(e => e.LastLoginAt)
               .IsRequired();

        // Exclude JSON-ignored properties from the database model
        builder.Ignore(e => e.IsNullObject);

        // Exclude other non-mapped properties
        builder.Ignore(e => e.IsAuthenticated);
    }
}
