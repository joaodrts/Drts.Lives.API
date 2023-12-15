using Domain.Entities;
using Domain.Interfaces.Repositorys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class EnrollmentRepository() : IEnrollmentRepository
    {
        private readonly DbContextOptions<AppDbContext> _optionsBuilder = new();

        public async Task Add(Enrollment entity)
        {
            using var data = new AppDbContext(_optionsBuilder);
            await data.Set<Enrollment>().AddAsync(entity);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            using var data = new AppDbContext(_optionsBuilder);
            return await data.Set<Enrollment>().ToListAsync();
        }

        public async Task<Enrollment> GetByID(int id)
        {
            using var data = new AppDbContext(_optionsBuilder);
            return await data.Set<Enrollment>().FindAsync(id);
        }

        public async Task Remove(Enrollment entity)
        {
            using var data = new AppDbContext(_optionsBuilder);
            data.Set<Enrollment>().Remove(entity);
            await data.SaveChangesAsync();
        }

        public async Task Update(Enrollment entity)
        {
            using var data = new AppDbContext(_optionsBuilder);
            data.Set<Enrollment>().Update(entity);
            await data.SaveChangesAsync();
        }

        public async Task<bool> IsDuplicat(Enrollment entity)
        {
            using var data = new AppDbContext(_optionsBuilder);
            return await data.Set<Enrollment>().AnyAsync(x => x.live_id == entity.live_id 
                                                         && x.person_registered_id == entity.person_registered_id);
        }

    }
}
