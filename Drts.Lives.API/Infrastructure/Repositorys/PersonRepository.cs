using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositorys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DbContextOptions<AppDbContext> _optionsBuilder;

        public PersonRepository(AppDbContext context)
        {
            _optionsBuilder = new DbContextOptions<AppDbContext>();
        }

        public async Task Add(Person person)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                await data.Set<Person>().AddAsync(person);
                await data.SaveChangesAsync();
            }
        }

        public async Task Remove(Person person)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                data.Set<Person>().Remove(person);
                await data.SaveChangesAsync();
            }
        }

        public async Task Update(Person person)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                data.Set<Person>().Update(person);
                await data.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                return await data.Set<Person>()
                                 .Where(x => x.type == personType)
                                 .ToListAsync();
            }
        }

        public async Task<Person> GetByID(int id, PersonTypeEnum personType)
        {
            using (var data = new AppDbContext(_optionsBuilder))
            {
                return await data.Set<Person>()
                                 .Where(p => p.type == personType)
                                 .FirstOrDefaultAsync(p => p.id == id);
            }
        }
    }
}
