using DataModels.Entities;

namespace DataModels.DTO;

public class CreateCarDTO
{
    public CreateCarDTO(string licensePlate, int userId)
    {
        LicensePlate = licensePlate;
        UserId = userId;
    }

    public string LicensePlate { get; set; }
    public int UserId { get; set; }
}
