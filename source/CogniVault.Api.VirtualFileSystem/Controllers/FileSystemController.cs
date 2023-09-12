using CogniVault.Api.VirtualFileSystem.Dtos;
using CogniVault.Application.VirtualFileSystem.Contracts;
using CogniVault.Application.VirtualFileSystem.ValueObjects;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace CogniVault.Api.VirtualFileSystem.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FileSystemController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IVirtualFileSystem _fileSystemService;
    private readonly IValidator<DirectoryName> _directoryNameValidator;

    public FileSystemController(IServiceProvider provider, IVirtualFileSystem fileSystemService)
    {
        _serviceProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var fileSystem = await _fileSystemService.GetNodeAsync(id);


        return Ok(fileSystem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fileSystems = await _fileSystemService.GetNodeAsync(_fileSystemService.Root.Id);
        return Ok(fileSystems);
    }

    [HttpGet, Route("root")]
    public async Task<IActionResult> GetRoot()
    {
        var fileSystems = await _fileSystemService.GetRootNodeAsync();
        return Ok(fileSystems);
    }


    [HttpPost("{id:guid}/directory")]
    public async Task<IActionResult> CreateDirectory(Guid parentId, [FromBody] DirectoryNodeDto directoryNameDto)
    {
        if (parentId == Guid.Empty)
            return BadRequest("Parent id is required.");

        

        if (directoryNameDto == null || string.IsNullOrEmpty(directoryNameDto.Name.ToString()))
            return BadRequest("Directory name is required.");

        var existingFileSystem = await _fileSystemService.GetNodeAsync(parentId);
        if (existingFileSystem == null)
            return NotFound();

        var directoryName = await DirectoryName.CreateAsync(directoryNameDto.Name, _directoryNameValidator);
        var directory = await _fileSystemService.CreateDirectoryAsync(existingFileSystem.Id, directoryName);

        return CreatedAtAction("CreateDirectory", directory);
    }
}