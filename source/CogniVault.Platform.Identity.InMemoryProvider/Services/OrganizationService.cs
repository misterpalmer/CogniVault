using System.Reflection;
using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Specifications;
using CogniVault.Platform.Identity.ValueObjects;
using CogniVault.Platform.Core.Extensions;

namespace CogniVault.Platform.Identity.InMemoryProvider.Services;

public class OrganizationService : IPlatformOrganizationService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private IQueryRepositoryAsync<PlatformOrganization, Guid> OrganizationQueryRepository =>
        _unitOfWork.QueryRepository<PlatformOrganization, Guid>();
    
    private ICommandRepositoryAsync<PlatformOrganization, Guid> OrganizationCommandRepository =>
        _unitOfWork.CommandRepository<PlatformOrganization, Guid>();

    public async Task<PlatformOrganization> CreateOrganizationAsync(OrganizationName organizationName)
    {
        // Await the async CreateAsync method and assign it to 'organization'
        var organization = await PlatformOrganization.CreateAsync(organizationName);
        
        // This line is redundant as Name is already set inside PlatformOrganization.CreateAsync()
        // organization.Name = organizationName.Copy();

        await OrganizationCommandRepository.InsertAsync(organization);

        // Ideally you'd call this to emulate a commit to the "database"
        await _unitOfWork.CompleteAsync();

        return organization;
    }


    public async Task<PlatformOrganization> GetOrganizationAsync(Guid organizationId)
    {
        var spec = new GetByIdSpecification<PlatformOrganization, Guid>(organizationId);
        var organization = await OrganizationQueryRepository.GetFirstOrDefaultAsync(spec);
        
        if (organization == null)
            organization = PlatformOrganization.Null;

        return organization;
    }

    public async Task<IEnumerable<PlatformOrganization>> GetAllOrganizationsAsync()
    {
        var spec = new AllEntitiesSpecification<PlatformOrganization>();
        var asyncEnumerableResult = await OrganizationQueryRepository.GetAllAsync(spec);
        return await asyncEnumerableResult.ToListAsync();
    }

    public async Task UpdateOrganizationAsync(PlatformOrganization organization)
    {
        await OrganizationCommandRepository.UpdateAsync(organization);
        await _unitOfWork.CompleteAsync(); // "Commit" the changes
    }

    public async Task DeleteOrganizationAsync(Guid organizationId)
    {
        await OrganizationCommandRepository.DeleteAsync(organizationId);
        await _unitOfWork.CompleteAsync(); // "Commit" the deletion
    }
}
