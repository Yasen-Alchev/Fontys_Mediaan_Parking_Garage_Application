using DataModels.DTO;
using DataModels.Entities;

namespace Service.Contracts;

public interface IStayService
{
    Task<Stay> CreateStay(CreateStayDTO stayDTO);
    Task<IEnumerable<Stay>> GetStays();
    Task<Stay> GetStay(int stayId);
    Task UpdateStay(int stayId, DateTime? leaveTime);
    Task DeleteStay(int stayId);
}
