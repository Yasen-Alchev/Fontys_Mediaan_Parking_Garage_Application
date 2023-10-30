using DataAccess.Contracts;
using DataModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Parking_Garage_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "UserById")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDTO user)
    {
        try
        {
            var createdUser = await _userService.CreateUser(user);
            //return the URL to access it
            return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO user)
    {
        try
        {
            var dbUser = await _userService.GetUser(id);
            if (dbUser == null)
                return NotFound();
            await _userService.UpdateUser(id, user);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var dbUser = await _userService.GetUser(id);
            if (dbUser == null)
                return NotFound();
            await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}
