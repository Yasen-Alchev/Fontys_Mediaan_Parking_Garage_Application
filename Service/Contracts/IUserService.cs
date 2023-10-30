using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels.DTO;

namespace Service.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task<User> CreateUser(CreateUserDTO user);
        public Task UpdateUser(int id, UpdateUserDTO user);
        public Task DeleteUser(int id);
    }
}
