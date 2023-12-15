using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribedController(IPersonApplication person) : ControllerBase
    {
        private readonly IPersonApplication _person = person;

        [HttpPost("/api/subscribed")]
        public async Task<IActionResult> Create([FromBody] PersonDTO personDTO)
        {
            try
            {
                Person person = new()
                {
                    name = personDTO.name,
                    date_of_birth = personDTO.date_of_birth,
                    email = personDTO.email,
                    instagram = personDTO.instagram,
                    type = PersonTypeEnum.subscribed
                };

                if (await _person.DuplicatEmail(person)) return BadRequest(new { ErrorMessage = $"Email is already in use. ({person.email}) " });
                if (await _person.DuplicatInstagram(person)) return BadRequest(new { ErrorMessage = $"Instagram is already in use. ({person.instagram})" });

                await _person.Add(person);

                return Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/subscribed/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PersonDTO personDTO)
        {
            try
            {
                Person personOld = await _person.GetByID(id, PersonTypeEnum.subscribed);
                if (personOld == null) return NotFound(new {ErrorMessage = $"Subscribed not found. (id {id})" });

                Person person = new()
                {
                    id = id,
                    name = personDTO.name,
                    date_of_birth = personDTO.date_of_birth,
                    email = personDTO.email,
                    instagram = personDTO.instagram,
                    type = PersonTypeEnum.subscribed
                };

                if (await _person.DuplicatEmail(person)) return BadRequest(new { ErrorMessage = $"Email is already in use. ({person.email}) " });
                if (await _person.DuplicatInstagram(person)) return BadRequest(new { ErrorMessage = $"Instagram is already in use. ({person.instagram})" });

                await _person.Update(person);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/subscribed/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Person person = await _person.GetByID(id, PersonTypeEnum.subscribed);
                if (person == null) return NotFound(new { ErrorMessage = $"Subscribed not found. (id {id})" });

                await _person.Remove(person);

                return Ok("Successfully removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/subscribed")]
        public async Task<IEnumerable<Person>> Get()
        {
            try
            {
                return await _person.GetAll(PersonTypeEnum.subscribed);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Person>();
            }
        }

        [HttpGet("/api/subscribed/{id}")]
        public async Task<Person> GetByID(int id)
        {
            try
            {
                return await _person.GetByID(id, PersonTypeEnum.subscribed);
            }
            catch (Exception)
            {
                return (Person)Enumerable.Empty<Person>();
            }
        }

    }
}
