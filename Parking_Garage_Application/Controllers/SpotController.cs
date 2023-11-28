using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using DataModels.Entities;
using DataModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parking_Garage_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpotController : ControllerBase
{
    private readonly ISpotService _spotService;

    public SpotController(ISpotService spotService)
    {
        _spotService = spotService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Spot>>> GetSpots()
    {
        try
        {
            var spots = await _spotService.GetSpots();
            return Ok(spots);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet("{spotId}")]
    public async Task<ActionResult<Spot>> GetSpot(int spotId)
    {
        try
        {
            var spot = await _spotService.GetSpot(spotId);
            if (spot == null)
            {
                return NotFound();
            }

            return Ok(spot);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Spot>> CreateSpot([FromBody] CreateSpotDTO spotDTO)
    {
        try
        {
            var createdSpot = await _spotService.CreateSpot(spotDTO);
            return CreatedAtAction(nameof(GetSpot), new { spotId = createdSpot.Id }, createdSpot);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpot(int id, [FromBody] UpdateSpotDTO spotDTO)
    {
        try
        {
            await _spotService.UpdateSpot(spotDTO);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpot(int id)
    {
        try
        {
            await _spotService.DeleteSpot(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
