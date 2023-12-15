using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController(IPersonApplication person) : ControllerBase
    {
        private readonly IPersonApplication _person = person;

        [HttpPost("/api/instructors")]
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
                    type = PersonTypeEnum.instructor
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

        [HttpPut("/api/instructors/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PersonDTO personDTO)
        {
            try
            {
                Person personOld = await _person.GetByID(id, PersonTypeEnum.instructor);
                if (personOld == null) return NotFound(new {ErrorMessage = $"Instructor not found. (id {id})"});

                Person person = new()
                {
                    id = id,
                    name = personDTO.name,
                    date_of_birth = personDTO.date_of_birth,
                    email = personDTO.email,
                    instagram = personDTO.instagram,
                    type = PersonTypeEnum.instructor
                };

                if (await _person.DuplicatEmail(person)) return BadRequest(new { ErrorMessage = $"Email is already in use. ({person.email})" });
                if (await _person.DuplicatInstagram(person)) return BadRequest(new { ErrorMessage = $"Instagram is already in use. ({person.instagram})" });

                await _person.Update(person);

                return Ok("Updated successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/instructors/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Person person = await _person.GetByID(id, PersonTypeEnum.instructor);
                if (person == null) return NotFound(new { ErrorMessage = $"Instructor not found. (id {id})" });

                await _person.Remove(person);

                return Ok("Successfully removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/instructors")]
        public async Task<IEnumerable<Person>> Get()
        {
            try
            {
                return await _person.GetAll(PersonTypeEnum.instructor);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Person>();
            }
        }

        [HttpGet("/api/instructors/{id}")]
        public async Task<Person> GetByID(int id)
        {
            try
            {
                return await _person.GetByID(id, PersonTypeEnum.instructor);
            }
            catch (Exception)
            {
                return (Person)Enumerable.Empty<Person>();
            }
        }

    }
}
