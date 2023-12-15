using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces.Repositorys
{
    public interface IPersonRepository
    {
        Task Add(Person person);
        Task Update(Person person);
        Task Remove(Person person);
        Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType);
        Task<Person> GetByID(int id, PersonTypeEnum personType);
        Task<bool> DuplicatEmail(Person person);
        Task<bool> DuplicatInstagram(Person person);
    }
}
