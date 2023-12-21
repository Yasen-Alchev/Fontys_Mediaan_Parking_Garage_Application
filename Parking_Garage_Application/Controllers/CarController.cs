using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Parking_Garage_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICarService _carService;

    public CarController(IUserService userService, ICarService carService)
    {
        _userService = userService;
        _carService = carService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        try
        {
            var cars = await _carService.GetCars();
            return Ok(cars);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        try
        {
            var car = await _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("licenseplate/{licensePlate}")]
    public async Task<ActionResult<Car>> GetCarByLicensePlate(string licensePlate)
    {
        try
        {
            var car = await _carService.GetCarByLicensePlate(licensePlate);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Car>> CreateCar([FromBody] CreateCarDTO carDTO)
    {
        try
        {
            var createdCar = await _carService.CreateCar(carDTO);
            return CreatedAtAction(nameof(GetCarById), new { id = createdCar.Id}, createdCar);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarDTO carDTO)
    {
        try
        {
            await _carService.UpdateCar(id, carDTO);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        try
        {
            await _carService.DeleteCar(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost("entry")]
    public async Task<ActionResult<Stay>> CarEntry([FromBody] CreateCarDTO entryDTO)
    {
        try
        {
            var user = await _userService.GetUser(entryDTO.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var stay = await _carService.CarEntry(entryDTO.UserId, entryDTO.LicensePlate);
            if(stay == null)
            {
                return BadRequest("Car is not allowed to enter. Not parking spots available at the moment!");
            }
            return Ok(stay);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost("leave")]
    public async Task<ActionResult<Stay>> CarLeave([FromBody] CarLeaveDTO leaveDTO)
    {
        try
        {
            if (await _carService.IsCarAllowedToLeave(leaveDTO.CarId))
            {
                var stay = await _carService.CarLeave(leaveDTO.CarId);
                if (stay == null)
                {
                    return BadRequest("Car is not allowed to leave! Check with the Payment Terminal machine!");
                }
                return Ok(stay);
            }
            else
            {
                return BadRequest("Car is not allowed to leave! Check with the Payment Terminal machine!");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
