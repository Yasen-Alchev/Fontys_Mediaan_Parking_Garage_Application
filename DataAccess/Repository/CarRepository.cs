using Dapper;
using DataAccess.Contracts;
using DataAccess.Context;
using DataModels.DTO;
using DataModels.Entities;
using System.Data;
using Newtonsoft.Json;

namespace DataAccess.Repository;

public class CarRepository : ICarRepository
{
    private readonly DapperContext _context;

    public CarRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetCars()
    {
        var query = "SELECT * FROM Car";
        using (var connection = _context.CreateConnection())
        {
            var cars = await connection.QueryAsync<Car>(query);
            return cars.ToList();
        }
    }

    public async Task<Car> GetCar(int userId)
    {
        var query = "SELECT * FROM Car WHERE user_id = @UserId;";
        using (var connection = _context.CreateConnection())
        {
            try
            {
                var car = await connection.QuerySingleOrDefaultAsync<Car>(query, new { UserId = userId });

                // Log the SQL query and the retrieved car
                Console.WriteLine($"SQL Query: {query}");
                Console.WriteLine($"Retrieved Car: {JsonConvert.SerializeObject(car)}");

                return car;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetCar: {ex}");
                throw; // Rethrow the exception to maintain the existing error handling
            }
        }
    }

    public async Task<Car> CreateCar(CreateCarDTO car)
    {
        var query = "INSERT INTO Car (license_plate, user_id) " +
                    "VALUES (@LicensePlate, @UserId); " +
                    "SELECT LAST_INSERT_ID();";

        var parameters = new DynamicParameters();
        parameters.Add("LicensePlate", car.LicensePlate, DbType.String);
        parameters.Add("UserId", car.UserId, DbType.Int32);

        using (var connection = _context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, parameters);

            var createdCar = new Car
            {
                Id = id,
                LicensePlate = car.LicensePlate,
                UserId = car.UserId,
            };
            return createdCar;
        }
    }

    public async Task UpdateCar(int id, UpdateCarDTO car)
    {
        var query = "UPDATE Car " +
                    "SET license_plate = @LicensePlate, " +
                    "user_id = @UserId " +
                    "WHERE id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Int32);
        parameters.Add("LicensePlate", car.LicensePlate, DbType.String);
        parameters.Add("UserId", car.UserId, DbType.Int32);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteCar(int id)
    {
        var query = "DELETE FROM Car WHERE id = @Id";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
