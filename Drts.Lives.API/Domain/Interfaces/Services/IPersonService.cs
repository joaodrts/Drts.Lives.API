using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Services
{
    public interface IPersonService
    {
        Task Add(Person person);
        Task Update(Person person);
        Task Remove(Person person);
        Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType);
        Task<Person> GetByID(int id, PersonTypeEnum personType);
    }
}
