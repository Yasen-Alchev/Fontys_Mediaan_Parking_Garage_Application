using Dapper;
using DataAccess.Contracts;
using DataAccess.Context;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DataModels.DTO;

namespace DataAccess.Repository;

public class StayRepository : IStayRepository
{
    private readonly DapperContext _context;

    public StayRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Stay>> GetStays()
    {
        var query = "SELECT * FROM Stay";
        using (var connection = _context.CreateConnection())
        {
            var stays = await connection.QueryAsync<Stay>(query);
            return stays;
        }
    }

    public async Task<Stay> GetStay(int carId)
    {
        var query = "SELECT S1.id, entry_time, leave_time, car_id FROM `stay` as S1 " +
                    "INNER JOIN " +
                    "(SELECT MAX(id) AS id FROM `stay` WHERE car_id = @CarId) as S2 " +
                    "ON S1.id = S2.id;";

        using (var connection = _context.CreateConnection())
        {
            var stay = await connection.QuerySingleOrDefaultAsync<Stay>(query, new { CarId = carId });
            return stay;
        }
    }

    public async Task<Stay> CreateStay(CreateStayDTO stay)
    {
        var query = "INSERT INTO Stay (entry_time, leave_time, car_id) " +
                    "VALUES (@EntryTime, @LeaveTime, @CarId); " +
                    "SELECT LAST_INSERT_ID();";

        var parameters = new DynamicParameters();
        parameters.Add("EntryTime", stay.EntryTime, DbType.DateTime);
        parameters.Add("LeaveTime", stay.LeaveTime, DbType.DateTime);
        parameters.Add("CarId", stay.CarId, DbType.Int32);

        using (var connection = _context.CreateConnection())
        {
            try
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdStay = new Stay
                {
                    Id = id,
                    EntryTime = stay.EntryTime,
                    LeaveTime = stay.LeaveTime,
                    CarId = stay.CarId,
                };
                return createdStay;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating stay: {ex.Message}");
                throw;
            }
        }
    }



    public async Task UpdateStay(Stay stay)
    {
        var query = "UPDATE Stay " +
                    "SET entry_time = @EntryTime, " +
                    "leave_time = @LeaveTime, " +
                    "car_id = @CarId " +
                    "WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", stay.Id, DbType.Int32);
        parameters.Add("EntryTime", stay.EntryTime, DbType.DateTime);
        parameters.Add("LeaveTime", stay.LeaveTime, DbType.DateTime);
        parameters.Add("CarId", stay.CarId, DbType.Int32);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteStay(int stayId)
    {
        var query = "DELETE FROM Stay WHERE Id = @StayId";
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { StayId = stayId });
        }
    }
}
