namespace Parking_Garage_Application.Entities;

public class Spot
{
    public int Id { get; set; }
    public int Status { get; set; }
    public int ParkingId { get; set; }
    public int CarId { get; set; }
}
