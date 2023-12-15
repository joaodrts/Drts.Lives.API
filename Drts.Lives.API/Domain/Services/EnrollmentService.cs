using Domain.Entities;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(Enrollment entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(Enrollment entity)
        {
            await _repository.Update(entity);
        }

        public async Task Remove(Enrollment entity)
        {
            await _repository.Remove(entity);
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Enrollment> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }

        public async Task<bool> IsDuplicat(Enrollment entity)
        {
            return await _repository.IsDuplicat(entity);
        }
    }

}
