using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservation(CreateReservationDTO reservation);
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation> GetReservation(int reservationId);
    }
}