using Dapper;
using DataAccess.Context;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;

namespace DataAccess.Repository
{
    public class SpotRepository : ISpotRepository
    {
        private readonly DapperContext _context;

        public SpotRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Spot> CreateSpot(CreateSpotDTO spot)
        {
            var query = @"INSERT INTO Spot (status, parking_id) 
                          VALUES (@Status, @ParkingId);
                          SELECT LAST_INSERT_ID();";

            using (var connection = _context.CreateConnection())
            {
                var spotId = await connection.ExecuteScalarAsync<int>(query, new { spot.Status, spot.ParkingId });
                return await GetSpot(spotId);
            }
        }

        public async Task DeleteSpot(int spotId)
        {
            var query = "DELETE FROM Spot WHERE id = @SpotId;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { SpotId = spotId });
            }
        }

        public async Task<Spot> GetSpot(int spotId)
        {
            var query = "SELECT * FROM Spot WHERE id = @SpotId;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Spot>(query, new { SpotId = spotId });
            }
        }

        public async Task<IEnumerable<Spot>> GetSpots()
        {
            var query = "SELECT * FROM Spot;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Spot>(query);
            }
        }

        public async Task UpdateSpot(UpdateSpotDTO spot)
        {
            var query = @"UPDATE Spot
                          SET status = @Status,
                              car_id = @CarId
                          WHERE id = @SpotId;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { spot.Status, CarId = (int?)spot.CarId, SpotId = spot.Id });
            }
        }
    }
}
