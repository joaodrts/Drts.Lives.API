using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Services;

namespace Application
{
    public class ApplicationServiceEnrollment(IEnrollmentService service) : IEnrollmentApplication
    {
        private readonly IEnrollmentService _service = service;

        public async Task Add(Enrollment entity)
        {
            await _service.Add(entity);
        }

        public async Task Update(Enrollment entity)
        {
            await _service.Update(entity);
        }

        public async Task Remove(Enrollment entity)
        {
            await _service.Remove(entity);
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            return await _service.GetAll();
        }

        public async Task<Enrollment> GetByID(int id)
        {
            return await _service.GetByID(id);
        }

        public async Task<bool> IsDuplicat(Enrollment entity)
        {
            return await _service.IsDuplicat(entity);
        }
    }
}
