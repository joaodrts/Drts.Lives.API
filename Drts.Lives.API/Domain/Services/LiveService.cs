using Domain.Entities;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class LiveService(ILiveRepository liveRepository) : ILiveService
    {
        private readonly ILiveRepository _liveRepository = liveRepository;

        public async Task Add(Live live)
        {
            await _liveRepository.Add(live);
        }

        public async Task Remove(Live live)
        {
            await _liveRepository.Remove(live);
        }

        public async Task Update(Live live)
        {
            await _liveRepository.Update(live);
        }

        public async Task<IEnumerable<Live>> GetAll()
        {
            return await _liveRepository.GetAll();
        }

        public async Task<Live> GetByID(int id)
        {
            return await _liveRepository.GetByID(id);
        }
    }
}
