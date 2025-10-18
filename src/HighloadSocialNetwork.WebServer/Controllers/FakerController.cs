using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.Fakers;
using Microsoft.AspNetCore.Mvc;

namespace HighloadSocialNetwork.WebServer.Controllers;

[ApiController]
[Route("api/v1/faker")]
public class FakerController(IHostEnvironment env, IFakeRepository fakeRepository) : ControllerBase
{
    [HttpGet("fake-users")]
    public async Task<IActionResult> FakeUsers([FromQuery] int? amount = 1_000_000)
    {
        if (!env.IsDevelopment())
        {
            return NotFound();
        }
        
        var count = await fakeRepository.GetCountOfUsers();
        if (count > 0)
        {
            return Ok($"There are already {count} users in the db.");
        }
        
        var faker = new UserInDbFaker();
        for (var i = 0; i < amount; i += FakerConstants.BatchSize)
        {
            var fakeUsers = faker.Generate(FakerConstants.BatchSize);
            await fakeRepository.InsertUsers(fakeUsers);
        }
        
        return Ok($"{amount} users has been successfully registered.");
    }
}