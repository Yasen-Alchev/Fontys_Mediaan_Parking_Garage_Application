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
    public Task<Car> GetCarById(int id);
    public Task<Car> GetCarByLicensePlate(string licensePlate);
    public Task<Car> CreateCar(CreateCarDTO car);
    public Task UpdateCar(int id, UpdateCarDTO car);
    public Task DeleteCar(int id);
}

