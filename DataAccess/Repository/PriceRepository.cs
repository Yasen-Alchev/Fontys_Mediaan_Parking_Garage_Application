using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Contracts;
using DataAccess.Context;
using Newtonsoft.Json;

namespace DataAccess.Repository
{
    public class PriceRepository : IPriceRepository
    {
        private readonly DapperContext _context;

        public PriceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Price> GetClosestOldestPrice(DateTime startTime)
        {
            var query = "SELECT * FROM Price WHERE effective_date <= @StartTime ORDER BY effective_date DESC LIMIT 1;";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var price = await connection.QuerySingleOrDefaultAsync<Price>(query, new { StartTime = startTime });

                    Console.WriteLine($"SQL Query: {query}");
                    Console.WriteLine($"Retrieved Price: {JsonConvert.SerializeObject(price)}");

                    return price;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetClosestOldestPrice: {ex}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Price>> GetPrices(int parkingId)
        {
            var query = "SELECT * FROM Price WHERE parking_id = @ParkingId;";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var prices = await connection.QueryAsync<Price>(query, new { ParkingId = parkingId });

                    // Log the SQL query and the retrieved prices
                    Console.WriteLine($"SQL Query: {query}");
                    Console.WriteLine($"Retrieved Prices: {JsonConvert.SerializeObject(prices)}");

                    return prices.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetPrices: {ex}");
                    throw; // Rethrow the exception to maintain the existing error handling
                }
            }
        }

        public async Task<Price> GetPriceById(int id)
        {
            var query = "SELECT * FROM Price WHERE id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var price = await connection.QuerySingleOrDefaultAsync<Price>(query, new { Id = id });

                    // Log the SQL query and the retrieved price
                    Console.WriteLine($"SQL Query: {query}");
                    Console.WriteLine($"Retrieved Price: {JsonConvert.SerializeObject(price)}");

                    return price;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in GetPriceById: {ex}");
                    throw; // Rethrow the exception to maintain the existing error handling
                }
            }
        }

        public async Task<Price> CreatePrice(CreatePriceDTO priceDTO)
        {
            var query = "INSERT INTO Price (amount, parking_id, effective_date) " +
                        "VALUES (@Amount, @ParkingId, @EffectiveDate); " +
                        "SELECT LAST_INSERT_ID();";

            var parameters = new DynamicParameters();
            parameters.Add("Amount", priceDTO.Amount, DbType.Decimal);
            parameters.Add("ParkingId", priceDTO.ParkingId, DbType.Int32);
            parameters.Add("EffectiveDate", priceDTO.EffectiveDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdPrice = new Price
                {
                    Id = id,
                    Amount = priceDTO.Amount,
                    ParkingId = priceDTO.ParkingId,
                    EffectiveDate = priceDTO.EffectiveDate,
                };
                return createdPrice;
            }
        }


        public async Task UpdatePrice(int id, UpdatePriceDTO priceDTO)
        {
            var query = "UPDATE Price " +
                        "SET amount = @Amount, " +
                        "parking_id = @ParkingId, " +
                        "effective_date = @EffectiveDate " +
                        "WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Amount", priceDTO.Amount, DbType.Decimal);
            parameters.Add("ParkingId", priceDTO.ParkingId, DbType.Int32);
            parameters.Add("EffectiveDate", priceDTO.EffectiveDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


        public async Task DeletePrice(int id)
        {
            var query = "DELETE FROM Price WHERE id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
