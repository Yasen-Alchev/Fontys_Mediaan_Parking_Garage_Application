using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Service.Contracts;

namespace Service;

public class UserService : IUserService
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
            throw;
        }
    }

    public async Task<User> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<User> CreateUser(CreateUserDTO user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "Missing user");

        try
        {
            // Check if the user already exists by email
            var existingUser = await _userRepository.GetUserByEmail(user.Email);

            if (existingUser != null)
            {
                // User already exists, return the existing user's ID
                return existingUser;
            }

            // User doesn't exist, proceed with creating a new user
            var createdUser = await _userRepository.CreateUser(user);
            return createdUser;
        }
        catch (Exception ex)
        {
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
            if (!isSuccessfullyUpdated) throw new ArgumentException("User with the following id was not found.");
        }
        catch (Exception ex)
        {
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
            throw;
        }
    }
}
