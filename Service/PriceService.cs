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

namespace Service;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;

    public async Task<double> CalculateStayCosts(DateTime startTime, DateTime endTime)
    {
        try
        {
            if (startTime >= endTime)
            {
                throw new ArgumentException("Start time must be before end time.");
            }

            var priceRecord = await _priceRepository.GetClosestOldestPrice(startTime);

            if (priceRecord == null)
            {
                System.Console.WriteLine("No price was set, so parking stay is free!");
                return 0.0;
            }

            TimeSpan duration = endTime - startTime;
            double totalCost = duration.TotalHours * priceRecord.Amount;

            return totalCost;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calculating stay costs: {ex.Message}");
            throw;
        }
    }



    public PriceService(IPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task<IEnumerable<Price>> GetPrices(int parkingId)
    {
        return await _priceRepository.GetPrices(parkingId);
    }

    public async Task<Price> GetPriceById(int id)
    {
        return await _priceRepository.GetPriceById(id);
    }

    public async Task<Price> CreatePrice(CreatePriceDTO priceDTO)
    {
        return await _priceRepository.CreatePrice(priceDTO);
    }

    public async Task UpdatePrice(int id, UpdatePriceDTO priceDTO)
    {
        await _priceRepository.UpdatePrice(id, priceDTO);
    }
    public async Task DeletePrice(int id)
    {
        await _priceRepository.DeletePrice(id);
    }
}
