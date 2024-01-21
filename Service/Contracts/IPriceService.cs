using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;

public interface IPriceService
{
    public Task<double> CalculateStayCosts(DateTime startTime, DateTime endTime);
    public Task<IEnumerable<Price>> GetPrices(int parkingId);
    public Task<Price> GetPriceById(int id);
    public Task<Price> CreatePrice(CreatePriceDTO priceDTO);
    public Task UpdatePrice(int id, UpdatePriceDTO priceDTO);
    public Task DeletePrice(int id);
}
