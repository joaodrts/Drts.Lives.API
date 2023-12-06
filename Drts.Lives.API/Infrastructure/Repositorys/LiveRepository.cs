using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositorys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorys
{
    public class LiveRepository : ILiveRepository
    {
        private readonly DbContextOptions<AppDbContext> _optionsBuilder;

        public LiveRepository(AppDbContext context)
        {
            _optionsBuilder = new DbContextOptions<AppDbContext>();
        }

        public async Task Add(Live live)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                await data.Set<Live>().AddAsync(live);
                await data.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Live>> GetAll()
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                return await data.Set<Live>().ToListAsync();
            }
        }

        public async Task<Live> GetByID(int id )
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                return await data.Set<Live>().FindAsync(id);
            }
        }

        public async Task Remove(Live live)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                data.Set<Live>().Remove(live);
                await data.SaveChangesAsync();
            }
        }

        public async Task Update(Live live)
        {
            using(var data = new AppDbContext(_optionsBuilder))
            {
                data.Set<Live>().Update(live);
                await data.SaveChangesAsync();
            }
        }
    }
}
