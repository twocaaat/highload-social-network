using HighloadSocialNetwork.WebServer.ApiContracts.Auth;
using HighloadSocialNetwork.WebServer.Exceptions;
using HighloadSocialNetwork.WebServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HighloadSocialNetwork.WebServer.Controllers;

[ApiController]
[Route("api/v1")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await authService.Login(request);
            return Ok(new LoginResponse(token));
        }
        catch (LoginException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("user/register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request) =>
        Ok(new RegisterResponse(await authService.CreateUser(request)));
}