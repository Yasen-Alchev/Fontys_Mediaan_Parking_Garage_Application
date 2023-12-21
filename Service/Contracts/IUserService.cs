using DataModels.DTO;
using DataModels.Entities;

namespace Service.Contracts;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int id);
    Task<User> GetUserByEmail(string email);
    Task<User> CreateUser(CreateUserDTO user);
    Task UpdateUser(int id, UpdateUserDTO user);
    Task DeleteUser(int id);
}
