using DataModels.DTO;
using DataModels.Entities;

namespace Service.Contracts;

public interface ICarService
{
    public Task<IEnumerable<Car>> GetCars();
    public Task<Car> GetCar(int userId);
    public Task<Car> CreateCar(CreateCarDTO car);
    public Task UpdateCar(int id, UpdateCarDTO car);
    public Task DeleteCar(int id);

    Task<bool> IsCarAllowedToLeave(int carId);

    Task<Stay> CarEntry(int userId, string licensePlate);
    Task<Stay> CarLeave(int carId);
}

