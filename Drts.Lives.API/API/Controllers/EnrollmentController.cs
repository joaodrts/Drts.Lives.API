using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentApplication _enrollment;
        private readonly IPersonApplication _person;
        private readonly ILiveApplication _live;

        public EnrollmentController(IEnrollmentApplication enrollment, 
                                    IPersonApplication person, 
                                    ILiveApplication live)
        {
            _enrollment = enrollment;
            _person = person;
            _live = live;

        }

        [HttpPost("/api/enrollment")]
        public async Task<IActionResult> Create([FromBody] Enrollment entity)
        {
            try
            {
                var live = await _live.GetByID(entity.live_id);
                if (live == null) return NotFound("Live not found");

                var subscribed = await _person.GetByID(entity.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound("Subscribed not found");

                await _enrollment.Add(entity);
                return StatusCode(201, "Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/enrollment/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Enrollment entity)
        {
            try
            {
                Enrollment enrollment = await _enrollment.GetByID(id);
                if (enrollment == null) return NotFound();

                var live = await _live.GetByID(entity.live_id);
                if (live == null) return NotFound("Live not found");

                var subscribed = await _person.GetByID(entity.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound("Subscribed not found");

                entity.id = id;
                await _enrollment.Update(entity);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/enrollment/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Enrollment enrollment = await _enrollment.GetByID(id);
                if (enrollment == null) return NotFound();

                await _enrollment.Remove(enrollment);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/enrollment")]
        public async Task<IEnumerable<Enrollment>> Get()
        {
            try
            {
                return await _enrollment.GetAll();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Enrollment>();
            }
        }

        [HttpGet("/api/enrollment/{id}")]
        public async Task<Enrollment> GetByID(int id)
        {
            try
            {
                return await _enrollment.GetByID(id);
            }
            catch (Exception ex)
            {
                return (Enrollment)Enumerable.Empty<Enrollment>();
            }
        }

    }
}
