using DataModels.Entities;

namespace DataModels.DTO;

public class UpdateCarDTO
{
    public UpdateCarDTO(string licensePlate, int userId, Spot? spot)
    {
        LicensePlate = licensePlate;
        UserId = userId;
        Spot = spot;
    }

    public string LicensePlate { get; set; }
    public int UserId { get; set; }
    public Spot? Spot { get; set; }
}
