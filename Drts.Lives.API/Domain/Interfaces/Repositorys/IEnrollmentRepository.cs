using Domain.Entities;

namespace Domain.Interfaces.Repositorys
{
    public interface IEnrollmentRepository
    {
        Task Add(Enrollment entity);
        Task Update(Enrollment entity);
        Task Remove(Enrollment entity);
        Task<IEnumerable<Enrollment>> GetAll();
        Task<Enrollment> GetByID(int id);
        Task<bool> IsDuplicat(Enrollment entity);
    }
}
