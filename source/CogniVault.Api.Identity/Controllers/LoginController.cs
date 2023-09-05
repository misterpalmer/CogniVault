using CogniVault.Platform.Identity.Abstractions;
using CogniVault.Platform.Identity.Services;
using CogniVault.Platform.Identity.ValueObjects;

using FluentValidation;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using System.Threading.Tasks;

namespace CogniVault.Api.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly TokenService _tokenService;
    private readonly LoginService _loginService;
    private readonly IPasswordEncryptor _encryptor;
    private readonly IValidator<Username> _usernameValidator;
    private readonly IValidator<PlainPassword> _passwordValidator;

    public LoginController(
        IAuthorizationService authorizationService,
        TokenService tokenService,
        LoginService loginService,
        IValidator<Username> usernameValidator,
        IValidator<PlainPassword> passwordValidator,
        IPasswordEncryptor encryptor)
    {
        _authorizationService = authorizationService;
        _tokenService = tokenService;
        _loginService = loginService;
        _usernameValidator = usernameValidator;
        _passwordValidator = passwordValidator;
        _encryptor = encryptor;
    }

    /// <summary>
    /// Authenticates the user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /authenticate
    ///     {
    ///       "username": "sampleUser",
    ///       "password": "samplePassword.23"
    ///     }
    ///
    /// Default values:
    /// 
    /// - username: "sampleUser"
    /// - password: "samplePassword.23"
    /// </remarks>
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromQuery, DefaultValue("sampleUser")] string usernameValue,
        [FromQuery, DefaultValue("samplePassword.23")] string passwordPlainText)
    {
        var username = await Username.CreateAsync(usernameValue, _usernameValidator);
        var password = await PlainPassword.CreateAsync(passwordPlainText, _passwordValidator);

        var authResult = await _authorizationService.AuthorizeUserAsync(HttpContext, username, password);

        if (authResult is OkObjectResult okResult)
        {
            return Ok(okResult.Value);
        }
        else if (authResult is IResult)
        {
            // Handle other types of IResult here if needed, or simply convert to an IActionResult
            return Ok(authResult);
        }

        return Unauthorized();
    }

    /// <summary>
    /// Generates a token for the user.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /generateToken
    ///     {
    ///     "username": "sampleUser"
    ///     }
    ///     
    /// Default values:
    /// 
    /// - username: "sampleUser"
    /// 
    /// </remarks>
    [HttpPost("generateToken")]
    public async Task<IActionResult> GenerateTokenAsync([FromQuery, DefaultValue("sampleUser")] string usernameValue)
    {
        var username = await Username.CreateAsync(usernameValue, _usernameValidator);
        var token = await _tokenService.GenerateTokenAsync(username);

        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest("Failed to generate a token.");
        }

        return Ok(new { Token = token });
    }

    /// <summary>
    /// Validates the token.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /validateToken?token=sampleToken
    ///     
    /// </remarks>
    [HttpGet("validateToken")]
    public IActionResult ValidateToken(string token)
    {
        var principal = _tokenService.ValidateToken(token);

        if (principal == null)
        {
            return BadRequest("Invalid token.");
        }

        return Ok(new { Status = "Token is valid." });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usernameValue"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /handleLogin
    ///     {
    ///     "username": "sampleUser"
    ///     }
    ///     
    /// Default values:
    /// 
    /// - username: "sampleUser"
    /// </remarks>
    [HttpPost("handleLogin")]
    public async Task<IActionResult> HandleLoginAsync([FromQuery, DefaultValue("sampleUser")] string usernameValue)
    {
        var username = await Username.CreateAsync(usernameValue, _usernameValidator);
        var loginResult = await _loginService.HandleLoginAsync(HttpContext, username);

        if (loginResult is RedirectResult redirectResult)
        {
            return Redirect(redirectResult.Url);
        }

        return BadRequest("Failed to handle the login.");
    }
}

