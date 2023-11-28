using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class SpotService : ISpotService
{
    private readonly ISpotRepository _spotRepository;

    public SpotService(ISpotRepository spotRepository)
    {
        _spotRepository = spotRepository;
    }

    public async Task<Spot> CreateSpot(CreateSpotDTO spot)
    {
        var spotEntity = new CreateSpotDTO
        {
            Status = spot.Status,
            ParkingId = spot.ParkingId,
        };

        return await _spotRepository.CreateSpot(spotEntity);
    }

    public async Task DeleteSpot(int spotId)
    {
        await _spotRepository.DeleteSpot(spotId);
    }

    public async Task<Spot> GetSpot(int spotId)
    {
        return await _spotRepository.GetSpot(spotId);
    }

    public async Task<IEnumerable<Spot>> GetSpots()
    {
        return await _spotRepository.GetSpots();
    }

    public async Task UpdateSpot(UpdateSpotDTO spot)
    {
        var spotEntity = new UpdateSpotDTO
        {
            Id = spot.Id,
            Status = spot.Status,
            CarId = spot.CarId
        };

        await _spotRepository.UpdateSpot(spotEntity);
    }
}
