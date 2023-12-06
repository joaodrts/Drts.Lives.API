using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IPersonApplication
    {
        Task Add(Person person);
        Task Update(Person person);
        Task Remove(Person person);
        Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType);
        Task<Person> GetByID(int id, PersonTypeEnum personType);
    }
}
