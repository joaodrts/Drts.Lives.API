using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application
{
    public class ApplicationServiceLive(ILiveService liveService) : ILiveApplication
    {
        private readonly ILiveService _liveService = liveService;

        public async Task Add(Live live)
        {
            await _liveService.Add(live);
        }

        public async Task<IEnumerable<Live>> GetAll()
        {
            return await _liveService.GetAll();
        }

        public Task<Live> GetByID(int id)
        {
            return _liveService.GetByID(id);
        }

        public async Task Remove(Live live)
        {
            await _liveService.Remove(live);
        }

        public async Task Update(Live live)
        {
            await _liveService.Update(live);
        }
    }
}
