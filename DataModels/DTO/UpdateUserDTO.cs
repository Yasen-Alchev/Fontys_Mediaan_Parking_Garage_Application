namespace DataModels.DTO;

public class UpdateUserDTO
{
    public UpdateUserDTO(string name, string email, int role)
    {
        Name = name;
        Email = email;
        Role = role;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
}
