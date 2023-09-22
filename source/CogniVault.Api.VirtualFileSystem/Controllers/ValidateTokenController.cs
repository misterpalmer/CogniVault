using System.Net.Http.Headers;
using System.Text.Json;
using CogniVault.Platform.Core.RestApi.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CogniVault.Api.VirtualFileSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValidateTokenController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly HttpClient _httpClient;

    public ValidateTokenController(IServiceProvider provider, HttpClient httpClient)
    {
        _serviceProvider = provider ?? throw new ArgumentNullException(nameof(provider));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    [HttpGet, Route("organizations")]
    public async Task<IActionResult> GetOrganizations()
    {
        var token = "token";
        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7166/api/org"); // rename organization to organizations
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return BadRequest();
        }

        var content = await response.Content.ReadAsStreamAsync();
        var organizations = await JsonSerializer.DeserializeAsync<List<OrganizationDto>>(content, Common.DefaultJsonSerializerOptions);
        return Ok(organizations);
        // return Ok();
    }
}

internal class OrganizationDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}