using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using DataModels.Entities;
using DataModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service;

namespace Parking_Garage_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            try
            {
                var reservations = await _reservationService.GetReservations();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservation(int reservationId)
        {
            try
            {
                var reservation = await _reservationService.GetReservation(reservationId);
                if (reservation == null)
                {
                    return NotFound();
                }

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody] CreateReservationDTO reservationDto)
        {
            try
            {
                var createdReservation = await _reservationService.CreateReservation(reservationDto);
                return CreatedAtAction(nameof(GetReservation), new { reservationId = createdReservation.Id }, createdReservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}