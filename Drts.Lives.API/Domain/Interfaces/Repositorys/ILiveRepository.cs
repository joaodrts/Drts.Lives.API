using Domain.Entities;

namespace Domain.Interfaces.Repositorys
{
    public interface ILiveRepository
    {
        Task Add(Live live);
        Task Update(Live live);
        Task Remove(Live live);
        Task<IEnumerable<Live>> GetAll();
        Task<Live> GetByID(int id);
    }
}
