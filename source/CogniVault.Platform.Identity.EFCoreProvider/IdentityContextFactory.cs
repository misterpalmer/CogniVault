using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CogniVault.Platform.Identity.EFCoreProvider;

public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    private readonly IConfiguration _configuration;

    // For runtime
    public IdentityContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // For design-time
    public IdentityContextFactory()
    {
        _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .AddJsonFile("appsettings.Development.json")
           .Build();
    }

    public IdentityContext CreateDbContext(string[] args)
    {
        var connectionString = _configuration.GetConnectionString("IdentityDbConnection");
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new IdentityContext(optionsBuilder.Options);
    }
}
