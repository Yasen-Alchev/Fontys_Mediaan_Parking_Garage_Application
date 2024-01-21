namespace DataModels.Entities;

public class Price
{
    public int Id { get; set; }
    public int ParkingId { get; set; }
    public double Amount { get; set; }
    public DateTime EffectiveDate{ get; set; }
}


