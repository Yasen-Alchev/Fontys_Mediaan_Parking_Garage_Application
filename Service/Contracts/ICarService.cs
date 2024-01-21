using DataModels.DTO;
using DataModels.Entities;

namespace Service.Contracts;

public interface ICarService
{
    public Task<IEnumerable<Car>> GetCars();
    public Task<IEnumerable<Car>> GetCarsByUserId(int userId);
    public Task<Car> GetCarById(int id);
    public Task<Car> GetCarByLicensePlate(string licensePlate);
    public Task<Car> CreateCar(CreateCarDTO car);
    public Task UpdateCar(int id, UpdateCarDTO car);
    public Task DeleteCar(int id);
    Task<bool> IsCarAllowedToEnter(int carId);
    Task<bool> IsCarAllowedToLeave(int carId);

    Task<Stay?> CarEntry(int userId, string licensePlate);
    Task<Stay?> CarLeave(int carId);
}

