using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IPersonApplication _person;

        public InstructorsController(IPersonApplication person)
        {
            _person = person;
        }

        [HttpPost("/api/instructors")]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            try
            {
                person.type = PersonTypeEnum.instructor;
                await _person.Add(person);

                return Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/instructors/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Person person)
        {
            try
            {
                Person personOld = await _person.GetByID(id, PersonTypeEnum.instructor) ;

                if (personOld == null) return NotFound();

                person.id = id;
                person.type = PersonTypeEnum.instructor;

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

                if (person == null) return NotFound();

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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return (Person)Enumerable.Empty<Person>();
            }
        }

    }
}
