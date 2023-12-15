using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositorys;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task Add(Person person)
        {
            await _personRepository.Add(person);
        }        

        public async Task Remove(Person person)
        {
            await _personRepository.Remove(person);
        }

        public async Task Update(Person person)
        {
            await _personRepository.Update(person);
        }

        public async Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType)
        {
            return await _personRepository.GetAll(personType);
        }

        public async Task<Person> GetByID(int id, PersonTypeEnum personType)
        {
            return await _personRepository.GetByID(id, personType);
        }

        public async Task<bool> DuplicatEmail(Person person)
        {
            return await _personRepository.DuplicatEmail(person);
        }

        public async Task<bool> DuplicatInstagram(Person person)
        {
            return await _personRepository.DuplicatInstagram(person);
        }
    }
}
