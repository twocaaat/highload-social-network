using HighloadSocialNetwork.WebServer.ApiContracts.User;
using Microsoft.AspNetCore.Mvc;

namespace HighloadSocialNetwork.WebServer.Controllers;

[ApiController]
[Route("api/v1/user")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
public class UserController : ControllerBase
{
    [HttpGet("get/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Login([FromRoute] Guid userId) => throw new NotImplementedException();
}