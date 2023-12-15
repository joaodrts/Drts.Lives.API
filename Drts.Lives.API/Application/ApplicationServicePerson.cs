using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Services;

namespace Application
{
    public class ApplicationServicePerson : IPersonApplication
    {
        private readonly IPersonService _personService;

        public ApplicationServicePerson(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task Add(Person person)
        {
            await _personService.Add(person);
        }

        public async Task<IEnumerable<Person>> GetAll(PersonTypeEnum personType)
        {
            return await _personService.GetAll(personType);
        }

        public async Task<Person> GetByID(int id, PersonTypeEnum personType)
        {
            return await _personService.GetByID(id, personType);
        }

        public async Task Remove(Person person)
        {
            await _personService.Remove(person);
        }

        public async Task Update(Person person)
        {
            await _personService.Update(person);
        }

        public async Task<bool> DuplicatEmail(Person person)
        {
            return await _personService.DuplicatEmail(person);
        }

        public async Task<bool> DuplicatInstagram(Person person)
        {
            return await _personService.DuplicatInstagram(person);
        }

    }
}
