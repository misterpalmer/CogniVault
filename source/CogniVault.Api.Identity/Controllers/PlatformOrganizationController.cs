using CogniVault.Platform.Core.Abstractions.Persistence;
using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Entities;
using CogniVault.Platform.Identity.Validators;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace CogniVault.Api.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformOrganizationController : ControllerBase
{
    private readonly IPlatformOrganizationService _organizationService;
    // private readonly IQueryRepositoryAsync<PlatformOrganization, Guid> _organizationRepository;
    private readonly IValidator<OrganizationName> _organizationNameValidator;

    public PlatformOrganizationController(IPlatformOrganizationService organizationService,
        IQueryRepositoryAsync<PlatformOrganization, Guid> organizationRepository,
        IValidator<OrganizationName> organizationNameValidator)
    {
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
        // _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _organizationNameValidator = organizationNameValidator ?? throw new ArgumentNullException(nameof(organizationNameValidator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrganizationNameDto organizationNameDto)
    {
        // if (organizationNameDto == null || string.IsNullOrEmpty(organizationNameDto.Name))
        //     return BadRequest("Organization name is required.");

        var organizationName = await OrganizationName.CreateAsync(organizationNameDto.Name.Trim(), _organizationNameValidator);
        
        var organization = await _organizationService.CreateOrganizationAsync(organizationName);
        return CreatedAtAction(nameof(Get), new { id = organization.Id }, organization);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var organization = await _organizationService.GetOrganizationAsync(id);
        if (organization.IsNullObject)
            return NotFound();

        return Ok(organization);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var organizations = await _organizationService.GetAllOrganizationsAsync();
        return Ok(organizations);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrganizationNameDto organizationNameDto)
    {
        if (organizationNameDto == null || string.IsNullOrEmpty(organizationNameDto.Name))
            return BadRequest("Organization name is required.");

        var existingOrganization = await _organizationService.GetOrganizationAsync(id);
        if (existingOrganization == null)
            return NotFound();

        await existingOrganization.UpdateNameAsync(organizationNameDto.Name, _organizationNameValidator);
        await _organizationService.UpdateOrganizationAsync(existingOrganization);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existingOrganization = await _organizationService.GetOrganizationAsync(id);
        if (existingOrganization == null)
            return NotFound();

        await _organizationService.DeleteOrganizationAsync(id);
        return NoContent();
    }
}

public class OrganizationNameDto
{
    public string Name { get; set; }
}