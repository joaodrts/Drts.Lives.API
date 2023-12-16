using Domain.Entities;
using Domain.Interfaces.Repositorys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class LiveRepository() : ILiveRepository
    {
        private readonly DbContextOptions<AppDbContext> _optionsBuilder = new();

        public async Task Add(Live live)
        {
            using var data = new AppDbContext(_optionsBuilder);
            await data.Set<Live>().AddAsync(live);
            await data.SaveChangesAsync();
        }

        public async Task Remove(Live live)
        {
            using var data = new AppDbContext(_optionsBuilder);
            data.Set<Live>().Remove(live);
            await data.SaveChangesAsync();
        }

        public async Task Update(Live live)
        {
            using var data = new AppDbContext(_optionsBuilder);
            data.Set<Live>().Update(live);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Live>> GetAll()
        {
            using var data = new AppDbContext(_optionsBuilder);
            return await data.Set<Live>().ToListAsync();
        }

        public async Task<Live> GetByID(int id)
        {
            using var data = new AppDbContext(_optionsBuilder);
            return await data.Set<Live>().FindAsync(id);
        }
    }
}
