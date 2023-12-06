using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LiveService : ILiveService
    {
        private readonly ILiveRepository _liveRepository;
        public LiveService(ILiveRepository liveRepository)
        {
            _liveRepository = liveRepository;
        }
        public async Task Add(Live live)
        {
            await _liveRepository.Add(live);
        }

        public async Task<IEnumerable<Live>> GetAll()
        {
            return await _liveRepository.GetAll();
        }

        public async Task<Live> GetByID(int id)
        {
            return await _liveRepository.GetByID(id);
        }

        public async Task Remove(Live live)
        {
            await _liveRepository.Remove(live);
        }

        public async Task Update(Live live)
        {
            await _liveRepository.Update(live);
        }
    }
}
