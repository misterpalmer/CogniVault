using CogniVault.Platform.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CogniVault.Platform.Identity.EFCore.Configurations;

public class PlatformUserConfiguration : IEntityTypeConfiguration<PlatformUser>
{
    public void Configure(EntityTypeBuilder<PlatformUser> builder)
    {
        // Primary Key
        builder.HasKey(u => u.Id);

        // Properties
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255); // Assuming a max length for username, adjust accordingly

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(512); // Assuming this is a hashed password, adjust accordingly

        builder.Property(u => u.Email)
            .HasMaxLength(255); // Assuming a max length for email

        builder.Property(u => u.Quota)
            .IsRequired(); // Assuming Quota is a required field. Also, if it's a complex type, you'd need to specify how to handle it (Owned type or another entity).

        builder.Property(u => u.TimeZone)
            .IsRequired()
            .HasConversion(tz => tz.Id, id => TimeZoneInfo.FindSystemTimeZoneById(id)); // Convert TimeZoneInfo to string and vice versa

        builder.Property(u => u.LastLoginAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();

        // Indexes
        builder.HasIndex(u => u.Username).IsUnique(); // Assuming usernames are unique
        builder.HasIndex(u => u.Email); // Index for emails, but not necessarily unique if you allow multiple accounts with the same email
    }
}
