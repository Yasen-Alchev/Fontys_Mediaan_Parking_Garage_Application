using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.DTO;
using DataModels.Entities;

namespace DataAccess.Contracts;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();
    public Task<User> GetUser(int id);
    public Task<User> CreateUser(CreateUserDTO user);
    public Task<User> GetUserByEmail(string email);
    public Task<bool> UpdateUser(int id, UpdateUserDTO user);
    public Task<bool> DeleteUser(int id);
}
