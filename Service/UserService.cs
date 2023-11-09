using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Service.Contracts;

namespace Service
{
    public  class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return users;
            }
            catch (Exception ex)
            {
                //throw custom exceptions, describing the issue
                throw;
            }
        }

        public async Task<User> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUser(id);
                return user;
            }
            catch (Exception ex)
            {
                //throw custom exceptions, describing the issue
                throw;
            }
        }

        public async Task<User> CreateUser(CreateUserDTO user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "Missing user");

            try
            {
                var createdUser = await _userRepository.CreateUser(user);
                return createdUser;
            }
            catch (Exception ex)
            {
                //throw custom exceptions, describing the issue
                throw;
            }
        }

        public async Task UpdateUser(int id, UpdateUserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Invalid data was provided to update user.");
            try
            {
                var isSuccessfullyUpdated = await _userRepository.UpdateUser(id, user);
                if (!isSuccessfullyUpdated) throw new ArgumentException("User with a following id was not found.");
            }
            catch (Exception ex)
            {
                //throw custom exceptions, describing the issue
                throw;
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var isSuccessfullyDeleted = await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                //throw custom exceptions, describing the issue
                throw;
            }
        }
    }
}
