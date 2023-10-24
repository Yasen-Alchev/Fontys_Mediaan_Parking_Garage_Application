namespace Parking_Garage_Application.Entities;

public class Car
{
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public int UserId { get; set; }
    public Spot Spot { get; set; }
}
