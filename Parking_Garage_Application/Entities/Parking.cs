namespace Parking_Garage_Application.Entities;

public class Parking
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Spot> spots { get; set; } = new List<Spot>();
}
