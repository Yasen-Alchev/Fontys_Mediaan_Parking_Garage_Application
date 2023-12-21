using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;

public interface ISpotService
{
    Task<Spot> CreateSpot(CreateSpotDTO spot);
    Task<IEnumerable<Spot>> GetSpots();
    Task<Spot> GetSpot(int spotId);
    Task UpdateSpot(UpdateSpotDTO spot);
    Task DeleteSpot(int spotId);
}
