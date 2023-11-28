using DataModels.DTO;
using DataModels.Entities;
using DataAccess.Contracts;
using Service.Contracts;

namespace Service
{
    public class StayService : IStayService
    {
        private readonly IStayRepository _stayRepository;

        public StayService(IStayRepository stayRepository)
        {
            _stayRepository = stayRepository;
        }

        public async Task<IEnumerable<Stay>> GetStays()
        {
            return await _stayRepository.GetStays();
        }

        public async Task<Stay> GetStay(int stayId)
        {
            return await _stayRepository.GetStay(stayId);
        }

        public async Task<Stay> CreateStay(CreateStayDTO stayDTO)
        {
            var newStay = new CreateStayDTO(
                DateTime.Now,
                stayDTO.LeaveTime,
                stayDTO.CarId
            );

            return await _stayRepository.CreateStay(newStay);
        }

        public async Task UpdateStay(int stayId, DateTime? leaveTime)
        {
            var existingStay = await _stayRepository.GetStay(stayId);

            if (existingStay == null)
            {
                throw new Exception($"Stay with ID {stayId} not found.");
            }

            existingStay.LeaveTime = leaveTime;

            await _stayRepository.UpdateStay(existingStay);
        }

        public async Task DeleteStay(int stayId)
        {
            await _stayRepository.DeleteStay(stayId);
        }
    }
}
