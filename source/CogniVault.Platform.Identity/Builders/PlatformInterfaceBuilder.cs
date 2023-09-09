using CogniVault.Platform.Core.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

namespace CogniVault.Platform.Identity.Builders;

public class PlatformInterfaceBuilder : IEntityBuilder<PlatformInterface>
{
    private readonly List<Func<Task>> _asyncTasks = new List<Func<Task>>();
    private PlatformTenant _tenant = PlatformTenant.Null;
    private InterfaceName _interfaceName = InterfaceName.Null;


    public PlatformInterfaceBuilder()
    {
        _asyncTasks.Add(async () => await Task.Delay(1));
    }

    public PlatformInterfaceBuilder WithTenant(PlatformTenant tenant)
    {
        _tenant = tenant;
        _asyncTasks.Add(async () => await ValidateTenantAsync(tenant));
        return this;
    }

    public PlatformInterfaceBuilder WithInterfaceName(InterfaceName interfaceName)
    {
        _interfaceName = interfaceName;
        _asyncTasks.Add(async () => await ValidateInterfaceNameAsync(interfaceName));
        return this;
    }


    public async Task<PlatformInterface> BuildAsync()
    {
        foreach (var task in _asyncTasks)
        {
            await task();
        }

        var platformInterface =  await PlatformInterface.CreateAsync(_tenant, _interfaceName);

        return platformInterface;
    }

    private async Task ValidateTenantAsync(PlatformTenant tenant)
    {
        await Task.Delay(1); // Simulate some async validation, e.g., database lookup
        if (tenant == PlatformTenant.Null)
        {
            throw new InvalidOperationException("Invalid tenant");
        }
    }

    private async Task ValidateInterfaceNameAsync(InterfaceName interfaceName)
    {
        await Task.Delay(1); // Simulate some async validation
        if (interfaceName == InterfaceName.Null)
        {
            throw new InvalidOperationException("Invalid interface name");
        }
    }
}
