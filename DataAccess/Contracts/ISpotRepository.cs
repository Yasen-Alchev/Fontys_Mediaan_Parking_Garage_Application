using DataModels.DTO;
using DataModels.Entities;

namespace DataAccess.Contracts;

public interface ISpotRepository
{
    Task<IEnumerable<Spot>> GetSpots();
    Task<Spot> GetSpot(int spotId);
    Task<Spot> CreateSpot(CreateSpotDTO spot);
    Task UpdateSpot(UpdateSpotDTO spot);
    Task DeleteSpot(int spotId);
}
