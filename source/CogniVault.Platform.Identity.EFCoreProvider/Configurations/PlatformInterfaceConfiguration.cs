using CogniVault.Platform.Identity.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CogniVault.Platform.Identity.EFCoreProvider.Configurations;

public class PlatformInterfaceConfiguration : IEntityTypeConfiguration<PlatformInterface>
{
    public void Configure(EntityTypeBuilder<PlatformInterface> builder)
    {
        throw new NotImplementedException();
    }
}