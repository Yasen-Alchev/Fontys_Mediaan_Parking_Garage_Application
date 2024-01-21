using Dapper;
using DataAccess.Context;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;

namespace DataAccess.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DapperContext _context;
        public ReservationRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservation(CreateReservationDTO reservation)
        {
            var query = @"INSERT INTO Reservation (car_id, spot_id, start_date, end_date) 
                          VALUES (@carId, @spotId, @startDate, @endDate);
                          SELECT LAST_INSERT_ID();";

            using (var connection = _context.CreateConnection())
            {
                var reservationId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    reservation.CarId, 
                    reservation.SpotId,
                    reservation.StartDate,
                    reservation.EndDate
                });

                return await GetReservation(reservationId);
            }
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            var query = "SELECT * FROM Reservation WHERE id = @ReservationId;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Reservation>(query, new { ReservationId = reservationId });
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var query = "SELECT * FROM Reservation;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Reservation>(query);
            }
        }
    }
}

