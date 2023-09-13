using CogniVault.Platform.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;

public class PlatformOrganizationConfiguration : IEntityTypeConfiguration<PlatformOrganization>
{
    public void Configure(EntityTypeBuilder<PlatformOrganization> builder)
    {
        throw new NotImplementedException();
    }
}