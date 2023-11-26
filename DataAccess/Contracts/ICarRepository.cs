using DataModels.DTO;
using DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts;

public interface ICarRepository
{
    public Task<IEnumerable<Car>> GetCars();
    public Task<Car> GetCar(int userId);
    public Task<Car> CreateCar(CreateCarDTO car);
    public Task UpdateCar(int id, UpdateCarDTO car);
    public Task DeleteCar(int id);
}

