using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Extensions;
using CogniVault.Platform.Identity.InMemoryProvider.Specifications;
using CogniVault.Platform.Identity.ValueObjects;

namespace CogniVault.Platform.Identity.InMemoryProvider.Services;


public class UserService : IPlatformUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private IQueryRepositoryAsync<PlatformUser, Guid> UserQueryRepository => _unitOfWork.QueryRepository<PlatformUser, Guid>();
    private ICommandRepositoryAsync<PlatformUser, Guid> UserCommandRepository => _unitOfWork.CommandRepository<PlatformUser, Guid>();
    

    public async Task<PlatformUser> CreateUserAsync(Username username,
        EncryptedPassword password,
        Email email,
        Quota quota,
        TimeZoneInfo timeZone,
        DateTimeOffset createdAt)
    {
        // var auditInfo = new AuditInfo(
        //     createdBy: Assembly.GetAssembly(typeof(PlatformUser)).FullName ?? "System",
        //     createdOnUtc: DateTimeOffset.UtcNow,
        //     modifiedBy: Assembly.GetAssembly(typeof(PlatformUser)).FullName ?? "System",
        //     modifiedOnUtc: DateTimeOffset.UtcNow
        // );

        var user = new PlatformUser(username, password, email, quota, timeZone, createdAt)
        {
            Id = Guid.NewGuid()
        };


        await UserCommandRepository.InsertAsync(user);

        // Ideally you'd call this to emulate a commit to the "database"
        await _unitOfWork.CompleteAsync();

        return user;
    }

    public async Task<bool> IsValidUserCredentialsAsync(Username username, PlainPassword password)
    {
        var user = await UserQueryRepository.GetFirstOrDefaultAsync(new GetByUsernameSpecification<PlatformUser, Guid>(username));
        return await Task.FromResult(user != null && user.Password.Verify(password));
    }

    public async Task<PlatformUser> GetUserAsync(Guid userId)
    {
        var spec = new GetByIdSpecification<PlatformUser, Guid>(userId);
        var user = await UserQueryRepository.GetFirstOrDefaultAsync(spec);
        
        if (user == null)
            user = PlatformUser.Null;

        return user;
    }

    public async Task<PlatformUser> GetByUsernameAsync(Username username)
    {
        var spec = new GetByUsernameSpecification<PlatformUser, Guid>(username);
        var user = await UserQueryRepository.GetFirstOrDefaultAsync(spec);
        
        if (user == null)
            user = PlatformUser.Null;

        return user;
    }

    public async Task<IEnumerable<PlatformUser>> GetAllOrganizationsAsync()
    {
        var spec = new AllEntitiesSpecification<PlatformUser>();
        var asyncEnumerableResult = await UserQueryRepository.GetAllAsync(spec);
        return await asyncEnumerableResult.ToListAsync();
    }

    public async Task UpdateOrganizationAsync(PlatformUser user)
    {
        await UserCommandRepository.UpdateAsync(user);
        await _unitOfWork.CompleteAsync(); // "Commit" the changes
    }

    public async Task DeleteOrganizationAsync(Guid organizationId)
    {
        await UserCommandRepository.DeleteAsync(organizationId);
        await _unitOfWork.CompleteAsync(); // "Commit" the deletion
    }
}