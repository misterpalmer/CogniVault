using Bogus;
using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Core.Extensions;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Builders;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.InMemoryProvider.Specifications;
using CogniVault.Platform.Identity.ValueObjects;
using FluentValidation;
using System.Security.Cryptography;
using System.Text.Json;


namespace CogniVault.Api.Identity.HostedServices;
public class PlatformSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    public IUnitOfWork UnitOfWork { get; private set; }
    public IValidator<Username> UsernameValidator { get; private set; }
    public IValidator<PlainPassword> PlainPasswordValidator { get; private set; }
    public IPasswordEncryptor PasswordEncryptor { get; private set; }
    public IValidator<Email> EmailValidator { get; private set; }
    public IValidator<Quota> QuotaValidator { get; private set; }
    public IValidator<OrganizationName> OrganizationNameValidator { get; private set; }
    public IValidator<TenantName> TenantNameValidator { get; private set; }
    public IValidator<InterfaceName> InterfaceNameValidator { get; private set; }

    public PlatformSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        // Initialize services and assign them to properties
        UnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        UsernameValidator = scope.ServiceProvider.GetRequiredService<IValidator<Username>>();
        PlainPasswordValidator = scope.ServiceProvider.GetRequiredService<IValidator<PlainPassword>>();
        PasswordEncryptor = scope.ServiceProvider.GetRequiredService<IPasswordEncryptor>();
        EmailValidator = scope.ServiceProvider.GetRequiredService<IValidator<Email>>();
        QuotaValidator = scope.ServiceProvider.GetRequiredService<IValidator<Quota>>();

        OrganizationNameValidator = scope.ServiceProvider.GetRequiredService<IValidator<OrganizationName>>();
        TenantNameValidator = scope.ServiceProvider.GetRequiredService<IValidator<TenantName>>();
        InterfaceNameValidator = scope.ServiceProvider.GetRequiredService<IValidator<InterfaceName>>();

        await SeedAdminUserAsync();
        await SeedUsersAsync();
        // await SeedOrganizationsAsync();
        // await SeedPlatformAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    public async Task SeedAdminUserAsync()
    {
        var username = await Username.CreateAsync("PlatformAdministrator", UsernameValidator);
        var plainPassword = await PlainPassword.CreateAsync("PlatformAdministrator.23", PlainPasswordValidator);
        var encryptedPassword = await EncryptedPassword.CreateAsync(plainPassword, PasswordEncryptor);
        var email = await Email.CreateAsync("admin@cognivault.io", EmailValidator);
        var quota = await Quota.CreateAsync(1000, QuotaValidator);
        var timeZone = TimeZoneInfo.Local;
        var currentDateTime = DateTimeOffset.UtcNow;

        var user = new PlatformUser(username, encryptedPassword, email, quota, timeZone, currentDateTime);
        await UnitOfWork.CommandRepository<PlatformUser, Guid>().InsertAsync(user);
        await UnitOfWork.CompleteAsync();
    }

    private async Task SeedUsersAsync()
    {
        int userCount = 10;

        var usernames = await GenerateAsync(userCount, async faker => {
            string username = GenerateValidUsername(faker);
            return await Username.CreateAsync(username, UsernameValidator) ?? Username.Null;
        });
        var passwords = await GenerateAsync(userCount, async faker => {
            string password = GenerateValidPassword(faker);
            return await EncryptedPassword.CreateAsync(await PlainPassword.CreateAsync(password, PlainPasswordValidator), PasswordEncryptor) ?? EncryptedPassword.Null;
        });
        var emails = await GenerateAsync(userCount, async faker => await Email.CreateAsync(faker.Internet.Email(), EmailValidator) ?? Email.Null);
        var quotas = await GenerateAsync(userCount, async faker => await Quota.CreateAsync(faker.Random.Int(100, 10000), QuotaValidator) ?? Quota.Null);
        
        var testUsers = Enumerable.Range(0, userCount).Select(i => new PlatformUser(
            usernames[i], 
            passwords[i], 
            emails[i], 
            quotas[i], 
            TimeZoneInfo.Local, 
            DateTimeOffset.UtcNow)
        ).ToList();

        await UnitOfWork.CommandRepository<PlatformUser, Guid>().InsertAsync(testUsers);
        await UnitOfWork.CompleteAsync();

        var queryRepository = await UnitOfWork.QueryRepository<PlatformUser, Guid>().GetAllAsync(new AllEntitiesSpecification<PlatformUser>());
        var seededUsers = await queryRepository.ToListAsync();
        // Console.WriteLine($"Seeded {seededUsers.Count} users.");
        // Console.WriteLine($"Seeded PlatformUsers: {JsonSerializer.Serialize(seededUsers)}");
    }
    
    public async Task SeedPlatformAsync()
    {
        int orgCount = 10;
        InterfaceName productionInterfaceName = await InterfaceName.CreateAsync("Production", InterfaceNameValidator) ?? InterfaceName.Null;
        InterfaceName developmentInterfaceName = await InterfaceName.CreateAsync("Development", InterfaceNameValidator) ?? InterfaceName.Null;

        var organizationNames = await GenerateAsync(orgCount, async faker => {
            string orgName = GenerateValidOrgNames(faker);
            return await OrganizationName.CreateAsync(orgName, OrganizationNameValidator) ?? OrganizationName.Null;
        });

        // Force synchronous evaluation
        var organizations = organizationNames.Select(async name => await PlatformOrganization.CreateAsync(name)).ToList();
        // Wait for all to complete and get actual PlatformOrganization entities
        var evaluatedOrganizations = organizations.Select(orgTask => orgTask.Result).ToList();

        foreach (var org in evaluatedOrganizations)
        {
            var tenantCount = RandomNumberGenerator.GetInt32(1, 4);

            var tenantNames = await GenerateAsync(tenantCount, async faker => {
                string tenantName = GenerateValidTenantNames(faker);
                return await TenantName.CreateAsync(tenantName, TenantNameValidator) ?? TenantName.Null;
            });

            foreach (var tenantName in tenantNames)
            {
                var tenantBuilder = new PlatformTenantBuilder()
                    .WithOrganization(org)
                    .WithTenantName(tenantName);

                var builtTenant = await tenantBuilder.BuildAsync();

                var productionInterfaceBuilder = new PlatformInterfaceBuilder()
                        .WithTenant(builtTenant)
                        .WithInterfaceName(productionInterfaceName);

                await builtTenant.AddInterfaceAsync(await productionInterfaceBuilder.BuildAsync());

                var developmentInterfaceBuilder = new PlatformInterfaceBuilder()
                        .WithTenant(builtTenant)
                        .WithInterfaceName(developmentInterfaceName);

                await builtTenant.AddInterfaceAsync(await developmentInterfaceBuilder.BuildAsync());
                
                await org.AddTenantAsync(builtTenant);
                // await UnitOfWork.CommandRepository<PlatformTenant, Guid>().InsertAsync(tenant);
            }
        }

        await UnitOfWork.CommandRepository<PlatformOrganization, Guid>().InsertAsync(evaluatedOrganizations);
        await UnitOfWork.CompleteAsync();

        var queryRepository = await UnitOfWork.QueryRepository<PlatformOrganization, Guid>().GetAllAsync(new AllEntitiesSpecification<PlatformOrganization>());
        var seededOrgs = await queryRepository.ToListAsync();
    }

    public async Task SeedOrganizationsAsync()
    {
        int orgCount = 10;

        var organizationNames = await GenerateAsync(orgCount, async faker => {
            string orgName = GenerateValidOrgNames(faker);
            return await OrganizationName.CreateAsync(orgName, OrganizationNameValidator) ?? OrganizationName.Null;
        });

        // Force synchronous evaluation
        var organizations = organizationNames.Select(async name => await PlatformOrganization.CreateAsync(name)).ToList();

        // Wait for all to complete and get actual PlatformOrganization entities
        var evaluatedOrganizations = organizations.Select(orgTask => orgTask.Result).ToList();

        await UnitOfWork.CommandRepository<PlatformOrganization, Guid>().InsertAsync(evaluatedOrganizations);
        await UnitOfWork.CompleteAsync();
    }

    private async Task<List<T>> GenerateAsync<T>(int count, Func<Faker, Task<T>> generatorFunc)
    {
        var faker = new Faker();
        
        var tasks = Enumerable.Range(0, count).Select(_ => generatorFunc(faker));

        return await Task.WhenAll(tasks).ContinueWith(t => t.Result.ToList());
    }

    private string GenerateValidUsername(Faker faker)
    {
        // Generate a username with Faker that matches your rules.
        // This example generates a username of length between 3 and 20 with alphanumeric characters and underscores.
        return faker.Random.String2(faker.Random.Int(11, 64), "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_");
    }

    private string GenerateValidPassword(Faker faker)
    {
        // Create a password that contains at least one special character
        string specialCharacters = "!@#$%^&*()-+";
        string digits = "0123456789";
        string lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string password = faker.Random.String2(9, upperCaseLetters) 
                        + faker.PickRandom(specialCharacters)
                        + faker.PickRandom(digits)
                        + faker.PickRandom(lowerCaseLetters);
        return password;
    }

    private string GenerateValidOrgNames(Faker faker)
    {
        // Generate a username with Faker that matches your rules.
        // This example generates a username of length between 3 and 20 with alphanumeric characters and underscores.
        return faker.Random.String2(faker.Random.Int(11, 64), "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_");
    }

    private string GenerateValidTenantNames(Faker faker)
    {
        // Generate a username with Faker that matches your rules.
        // This example generates a username of length between 3 and 20 with alphanumeric characters and underscores.
        return faker.Random.String2(faker.Random.Int(3, 16), "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_");
    }
}


// Console.WriteLine($"Seeded {seededOrgs.Count} orgs.");
//  Console.WriteLine($"Seeded PlatformOrganizationss: {JsonSerializer.Serialize(seededOrgs)}");

// // Force synchronous evaluation
// var tenants = tenantNames.Select(async name => await PlatformTenant.CreateAsync(org, name)).ToList();
// // Wait for all to complete and get actual PlatformOrganization entities
// var evaluatedTenants = tenants.Select(tenantTask => tenantTask.Result).ToList();


// var tenantBuilder = new PlatformTenantBuilder()
//     .WithOrganization(org)
//     .WithTenantName(await TenantName.CreateAsync("Default", OrganizationNameValidator));