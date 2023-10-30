using Dapper;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT * FROM User";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetUser(int id)
        {
            var query = "SELECT * FROM User WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return user;
            }
        }

        public async Task<User> CreateUser(CreateUserDTO user)
        {
            var query = "INSERT INTO User (name, email, password, age, role) " +
                "VALUES (@Name, @Email, @Password, @Age, @Role); " +
                "SELECT LAST_INSERT_ID();";

            var parameters = new DynamicParameters();
            parameters.Add("Name", user.Name, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Age", user.Age, DbType.Int32);
            parameters.Add("Role", user.Role, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdUser = new User
                {
                    Id = id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Age = user.Age,
                    Role = user.Role,
                };
                return createdUser;
            }
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO user)
        {
            var query = "UPDATE User " +
                "SET name = @Name, " +
                "email = @Email, " +
                "password = @Password, " +
                "age = @Age, " +
                "role = @Role " +
                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", user.Name, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Age", user.Age, DbType.Int32);
            parameters.Add("Role", user.Role, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, parameters);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            var query = "DELETE FROM User WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { id });
                return affectedRows > 0;
            }
        }

    }
}
