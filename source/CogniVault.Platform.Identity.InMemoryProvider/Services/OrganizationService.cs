using System.Reflection;
using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Specifications;
using CogniVault.Platform.Identity.ValueObjects;
using CogniVault.Platform.Identity.InMemoryProvider.Extensions;
using CogniVault.Platform.Core.Entities;

namespace CogniVault.Platform.Identity.InMemoryProvider.Services;

public class OrganizationService : IPlatformOrganizationService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private ICommandRepositoryAsync<PlatformOrganization, Guid> OrganizationCommandRepository => _unitOfWork.CommandRepository<PlatformOrganization, Guid>();
    private IQueryRepositoryAsync<PlatformOrganization, Guid> OrganizationQueryRepository => _unitOfWork.QueryRepository<PlatformOrganization, Guid>();

    public async Task<PlatformOrganization> CreateOrganizationAsync(OrganizationName organizationName)
    {
        var auditInfo = new AuditInfo(
        createdBy: Assembly.GetAssembly(typeof(PlatformOrganization)).FullName ?? "System",
        createdOnUtc: DateTimeOffset.UtcNow,
        modifiedBy: Assembly.GetAssembly(typeof(PlatformOrganization)).FullName ?? "System",
        modifiedOnUtc: DateTimeOffset.UtcNow
    );

    var organization = new PlatformOrganization(organizationName)
    {
        Id = Guid.NewGuid()
    };


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
