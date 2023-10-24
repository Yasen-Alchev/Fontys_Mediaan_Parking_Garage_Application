using Parking_Garage_Application.DTO;
using Parking_Garage_Application.Entities;

namespace Parking_Garage_Application.Contracts;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();
    public Task<User> GetUser(int id);
    public Task<User> CreateUser(CreateUserDTO user);
    public Task UpdateUser(int id, UpdateUserDTO user);
    public Task DeleteUser(int id);
}
