using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILiveApplication
    {
        Task Add(Live live);
        Task Update(Live live);
        Task Remove(Live live);
        Task<IEnumerable<Live>> GetAll();
        Task<Live> GetByID(int id);
    }
}
