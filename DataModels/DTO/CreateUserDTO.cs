namespace DataModels.DTO;

public class CreateUserDTO
{
    public CreateUserDTO(string name, string email, int role)
    {
        this.Name = name;
        this.Email = email;
        this.Role = role;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
}
