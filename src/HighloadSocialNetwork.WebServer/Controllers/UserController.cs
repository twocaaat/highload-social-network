using HighloadSocialNetwork.WebServer.ApiContracts.User;
using HighloadSocialNetwork.WebServer.DataAccess.Models;
using HighloadSocialNetwork.WebServer.Exceptions;
using HighloadSocialNetwork.WebServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HighloadSocialNetwork.WebServer.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/user")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("get/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid userId)
    {
        try
        {
            var user = await userService.GetUserById(userId);
            return Ok(user);
        }
        catch (EntityNotFoundException<UserInDb>)
        {
            return NotFound($"User {userId} not found.");
        }
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    public async Task<IActionResult> Search([FromQuery] string firstName, [FromQuery] string lastName) =>
        Ok(await userService.Search(firstName, lastName));
}