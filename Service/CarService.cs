using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataModels.DTO;
using DataModels.Entities;
using DataModels.Types;
using Service.Contracts;

namespace Service
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IStayService _stayService;
        private readonly ISpotService _spotService;

        public CarService(ICarRepository carRepository, IStayService stayService, ISpotService spotService)
        {
            _carRepository = carRepository;
            _stayService = stayService;
            _spotService = spotService;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _carRepository.GetCars();
        }
        public async Task<Car> GetCarById(int id)
        {
            return await _carRepository.GetCarById(id);
        }
        public async Task<Car> GetCarByLicensePlate(string licensePlate)
        {
            return await _carRepository.GetCarByLicensePlate(licensePlate);
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

        public async Task<bool> IsCarAllowedToEnter(int carId)
        {
            try
            {
                var spots = await _spotService.GetSpots();

                // Check if there is a spot with carId null and status Free
                var isAllowed = spots.Any(spot => spot.CarId == null && spot.Status == SpotStatus.Free);

                return isAllowed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> IsCarAllowedToLeave(int carId)
        {
            // TODO: Is stay paid -> depends on payment functionality - not implemented yet
            return true;
        }

        public async Task<Stay?> CarEntry(int userId, string licensePlate)
        {
            var car = await _carRepository.GetCarByLicensePlate(licensePlate);
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

            if (!await IsCarAllowedToEnter(car.Id))
            {
                return null;
            }

            var createStayDTO = new CreateStayDTO(DateTime.Now, null, car.Id);

            return await _stayService.CreateStay(createStayDTO);
        }

        public async Task<Stay?> CarLeave(int carId)
        {
            if (!await IsCarAllowedToLeave(carId))
            {
                return null;
            }
            else
            {
                var leaveTime = DateTime.Now;

                await _stayService.UpdateStay(carId, leaveTime);
                return await _stayService.GetStay(carId);
            }
        }


    }
}
