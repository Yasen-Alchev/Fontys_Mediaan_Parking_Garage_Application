using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using DataModels.Entities;
using System;
using System.Threading.Tasks;

namespace Parking_Garage_Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IStayService _stayService;
        private readonly ICarService _carService;


        public PaymentController(IStayService stayService, ICarService carService)
        {
            _stayService = stayService;
            _carService = carService;
        }

        [HttpGet("{carId}/calculate")]
        public async Task<ActionResult<decimal>> CalculatePayment(int carId, string licensePlate)
        {
            try
            {
                var stay = await _stayService.GetStay(carId);
                var car = await _carService.GetCarByLicensePlate(licensePlate);
                if (stay == null)
                {
                    return NotFound($"Stay with Car ID {carId} not found for payment calculation.");
                }

                decimal paymentAmount = CalculatePayment(stay.EntryTime, stay.LeaveTime, car.LicensePlate);

                return Ok(paymentAmount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private decimal CalculatePayment(DateTime? entryTime, DateTime? leaveTime, string licensePlate)
        {
            // Example hourly rate and additional fee for a specific license plate
            decimal hourlyRate = 10.00m;
            decimal licensePlateFee = 5.00m;

            if (entryTime.HasValue && leaveTime.HasValue)
            {
                TimeSpan duration = leaveTime.Value - entryTime.Value;
                int totalHours = (int)Math.Ceiling(duration.TotalHours);

                // Calculate the payment based on the hourly rate, total hours, and additional fee for the license plate
                decimal paymentAmount = (hourlyRate * totalHours) + licensePlateFee;

                return paymentAmount;
            }

            return 0.00m;
        }

        // Method to take account for paid reservation, to make sure it's excluded from the amount, 
        // in case the person came earlier or stayed later.

        [HttpGet("estimate")]
        public ActionResult<decimal> EstimatePayment(DateTime entryTime, DateTime leaveTime, string licensePlate)
        {
            decimal paymentAmount = CalculatePayment(entryTime, leaveTime, licensePlate);
            return Ok(paymentAmount);
        }

    }
}