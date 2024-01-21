using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataModels.DTO;

public class CreatePriceDTO
{
    public CreatePriceDTO(int parkingId, double amount, DateTime effectiveDate)
    {
        ParkingId = parkingId;
        Amount = amount;
        EffectiveDate = effectiveDate;
    }

    public int ParkingId { get; set; }
    public double Amount { get; set; }
    public DateTime EffectiveDate { get; set; }
}

