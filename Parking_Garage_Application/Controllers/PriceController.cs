using DataAccess.Contracts;
using DataAccess.Repository;
using DataModels.DTO;
using DataModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Contracts;

namespace Parking_Garage_Application.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PriceController: ControllerBase
{
    private readonly ICarService _carService;
    private readonly IStayService _stayService;
    private readonly IPriceService _priceService;

    public PriceController(ICarService carService, IStayService stayService, IPriceService priceService)
    {
        _carService = carService;
        _stayService = stayService;
        _priceService = priceService;
    }

    [HttpGet("payment/{licensePlate}")]
    public async Task<ActionResult<double>> CalculateStayCosts(string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate))
        {
            return BadRequest("License plate cannot be null or empty.");
        }

        try
        {
            Car car = await _carService.GetCarByLicensePlate(licensePlate);
            if (car == null)
            {
                return BadRequest($"Car with license plate {licensePlate} is not registered.");
            }

            Stay stay = await _stayService.GetStay(car.Id);
            if (stay == null)
            {
                return BadRequest($"Car with license plate {licensePlate} is not registered entering the parking.");
            }

            // The machine will calculate the price from the time of entering the parking until the time of the payment request.
            // Additional check should be added to the gate in case the customer does not leave the parking in 10-15 minutes.
            // A fee will be added if the customer does not leave the parking after the supposed time after paying the stay
            var price = await _priceService.CalculateStayCosts(stay.EntryTime, DateTime.Now);
            if (price == null)
            {
                return NotFound();
            }
            return Ok(price);
        }
        catch (Exception ex)
        {
            return Problem($"Internal Server Error: {ex.Message}", statusCode: 500);
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Price>> GetPriceById(int id)
    {
        try
        {
            var price = await _priceService.GetPriceById(id);
            if (price == null)
            {
                return NotFound();
            }
            return Ok(price);
        }
        catch (Exception ex)
        {
            return Problem($"Internal Server Error: {ex.Message}", statusCode: 500);
        }
    }

    [HttpGet("parking/{parkingId}")]
    public async Task<ActionResult<IEnumerable<Price>>> GetPricesByParkingId(int parkingId)
    {
        try
        {
            var prices = await _priceService.GetPrices(parkingId);
            return Ok(prices);
        }
        catch (Exception ex)
        {
            return Problem($"Internal Server Error: {ex.Message}", statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Price>> CreatePrice([FromBody] CreatePriceDTO priceDTO)
    {
        try
        {
            var createdPrice = await _priceService.CreatePrice(priceDTO);
            return CreatedAtAction(nameof(GetPriceById), new { id = createdPrice.Id }, createdPrice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceDTO priceDTO)
    {
        try
        {
            await _priceService.UpdatePrice(id, priceDTO);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrice(int id)
    {
        try
        {
            await _priceService.DeletePrice(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


}
