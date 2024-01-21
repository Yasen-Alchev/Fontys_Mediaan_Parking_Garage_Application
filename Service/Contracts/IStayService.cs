using DataModels.DTO;
using DataModels.Entities;

namespace Service.Contracts;

public interface IStayService
{
    Task<Stay> CreateStay(CreateStayDTO stayDTO);
    Task<IEnumerable<Stay>> GetStays();
    Task<Stay> GetStay(int carId);
    Task UpdateStay(int stayId, UpdateStayDTO updateStayDTO);
    Task DeleteStay(int stayId);
}
