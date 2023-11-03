namespace DataModels.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public List<Car> Cars { get; set; } = new List<Car>();
}
