using DataAccess.Contracts;
using DataAccess.Repository;
using DataModels.DTO;
using DataModels.Entities;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> CreateReservation(CreateReservationDTO reservation)
        {
            return await _reservationRepository.CreateReservation(reservation);
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            return await _reservationRepository.GetReservation(reservationId);
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationRepository.GetReservations();
        }
    }
}