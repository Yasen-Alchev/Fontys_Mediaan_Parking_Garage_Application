using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts;

public interface IStayRepository
{
    Task<IEnumerable<Stay>> GetStays();
    Task<Stay> GetStay(int stayId);
    Task<Stay> CreateStay(CreateStayDTO stay);
    Task UpdateStay(Stay stay);
    Task DeleteStay(int stayId);
}
