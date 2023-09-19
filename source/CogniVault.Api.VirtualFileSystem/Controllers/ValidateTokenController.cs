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
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGxhdGZvcm1BZG1pbmlzdHJhdG9yIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjE2OTUwMTY3MzYsImlzcyI6Iklzc3VlcjEiLCJhdWQiOiJBdWRpZW5jZTEifQ.RmZibZynacbPJNx2HYUB4wHUrxhEEnoyvPUfdfs2CMo";
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