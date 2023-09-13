using CogniVault.Platform.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;

public class PlatformTenantConfiguration : IEntityTypeConfiguration<PlatformTenant>
{
    public void Configure(EntityTypeBuilder<PlatformTenant> builder)
    {
        throw new NotImplementedException();
    }
}