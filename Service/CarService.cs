using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using Service.Contracts;

namespace Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IStayService _stayService;

        public CarService(ICarRepository carRepository, IStayService stayService)
        {
            _carRepository = carRepository;
            _stayService = stayService;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _carRepository.GetCars();
        }

        public async Task<Car> GetCar(int userId)
        {
            return await _carRepository.GetCar(userId);
        }

        public async Task<Car> CreateCar(CreateCarDTO car)
        {
            return await _carRepository.CreateCar(car);
        }

        public async Task UpdateCar(int id, UpdateCarDTO car)
        {
            await _carRepository.UpdateCar(id, car);
        }

        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteCar(id);
        }

        public async Task<bool> IsCarAllowedToLeave(int carId)
        {
            // TODO: Is stay paid -> depends on payment functionality - not implemented yet
            return true;
        }

        public async Task<Stay> CarEntry(int userId, string licensePlate)
        {
            var car = await _carRepository.GetCar(userId);
            if (car == null)
            {
                var createdCar = await _carRepository.CreateCar(new CreateCarDTO(licensePlate, userId));

                car = new Car
                {
                    Id = createdCar.Id,
                    LicensePlate = createdCar.LicensePlate,
                    UserId = createdCar.UserId
                };
            }

            var entryStay = new Stay
            {
                EntryTime = DateTime.Now,
                CarId = car.Id
            };

            var createStayDTO = new CreateStayDTO(DateTime.Now, null, car.Id);

            return await _stayService.CreateStay(createStayDTO);
        }

        public async Task<Stay> CarLeave(int carId)
        {
            if (await IsCarAllowedToLeave(carId))
            {
                var leaveTime = DateTime.Now;

                await _stayService.UpdateStay(carId, leaveTime);
                return await _stayService.GetStay(carId);
            }
            else
            {
                throw new InvalidOperationException("Car is not allowed to leave at this time.");
            }
        }

    }
}
