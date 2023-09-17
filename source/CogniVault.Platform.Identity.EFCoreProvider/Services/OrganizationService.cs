using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.EFCoreProvider.Specifications;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.EFCoreProvider.Services;

public class OrganizationService : IPlatformOrganizationService
{
    private readonly IDbResolver _dbResolver;
    private readonly IdentityContext _identityContext;

    public OrganizationService(IDbResolver dbResolver, IdentityContext context)
    {
        _dbResolver = dbResolver;
        _identityContext = context;
    }

    public async Task<PlatformOrganization> CreateOrganizationAsync(OrganizationName organizationName)
    {
        var organization = await PlatformOrganization.CreateAsync(organizationName);
        var organizations = new List<PlatformOrganization>()
        {
            organization
        };

        await _dbResolver.GetContext<PlatformOrganization>().InsertAsync(organizations);
        await _identityContext.SaveChangesAsync();

        return organization;
    }

    public async Task<IEnumerable<PlatformOrganization>> GetAllOrganizationsAsync()
    {
        // Using GetAllAsync method to fetch all organizations.
        return await _dbResolver.GetContext<PlatformOrganization>()
                                .GetAllAsync<PlatformOrganization>(new GetAllSpecification<PlatformOrganization>());
    }

    public async Task<IEnumerable<PlatformOrganization>> GetAllOrganizationsAsync(CancellationToken cancellationToken = default)
    {
        // Using GetAllAsync method to fetch all organizations.
        return await _dbResolver.GetContext<PlatformOrganization>()
                                .GetAllAsync<PlatformOrganization>(new GetAllSpecification<PlatformOrganization>());
    }


    public async Task<PlatformOrganization> GetOrganizationAsync(Guid organizationId, CancellationToken cancellationToken = default)
    {
        var spec = new GetByIdSpecification<PlatformOrganization>(organizationId);  // Assuming OrganizationByIdSpecification is a class that implements ISpecification<PlatformOrganization> and takes a Guid in constructor to set up criteria.
        var queryOptions = new QueryOptions<PlatformOrganization, PlatformOrganization>
        {
            // Set additional query options here if needed
            CancellationToken = cancellationToken
        };

        var organizations = (PlatformOrganization)await _dbResolver.GetContext<PlatformOrganization>().QueryAsync(spec, queryOptions, cancellationToken);
        return (PlatformOrganization)await _dbResolver.GetContext<PlatformOrganization>().QueryAsync(spec, queryOptions, cancellationToken);
    }

    public async Task<PlatformOrganization> GetOrganizationAsync(Guid organizationId)
    {
        return await GetOrganizationAsync(organizationId, default);
    }

    public Task UpdateOrganizationAsync(PlatformOrganization organization)
    {
        throw new NotImplementedException();
    }

    Task IPlatformOrganizationService.DeleteOrganizationAsync(Guid organizationId)
    {
        throw new NotImplementedException();
    }

    public async Task<PlatformOrganization> GetOrganizationByNameAsync(OrganizationName name, CancellationToken cancellationToken = default)
    {
        var spec = new GetByNameSpecification(name);
        var queryOptions = new QueryOptions<PlatformOrganization, PlatformOrganization>
        {
            // Set additional query options here if needed
            CancellationToken = cancellationToken
        };

        return (PlatformOrganization)await _dbResolver.GetContext<PlatformOrganization>().QueryAsync(spec, queryOptions, cancellationToken);
    }
}