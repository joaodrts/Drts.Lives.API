using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivesController : ControllerBase
    {
        private readonly ILiveApplication _live;
        private readonly IPersonApplication _person;

        public LivesController(ILiveApplication live, 
                               IPersonApplication person)
        {
            _live = live;
            _person = person;
        }

        [HttpPost("/api/lives")]
        public async Task<IActionResult> Create([FromBody] Live live)
        {
            try
            {
                var instructor = await _person.GetByID(live.instructor_id, PersonTypeEnum.instructor);
                if (instructor == null) return NotFound("Instructor not found");

                await _live.Add(live);

                return StatusCode(201, "Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/lives/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Live live)
        {
            try
            {
                var instructor = await _person.GetByID(live.instructor_id, PersonTypeEnum.instructor);
                if (instructor == null) return NotFound("Instructor not found");

                Live liveOld = await _live.GetByID(id);

                if (liveOld == null) return NotFound();

                live.id = id;
                await _live.Update(live);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/lives/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Live live = await _live.GetByID(id);
                if (live == null) return NotFound();

                await _live.Remove(live);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/lives")]
        public async Task<IEnumerable<Live>> Get()
        {
            try
            {
                return await _live.GetAll();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Live>();
            }
        }

        [HttpGet("/api/lives/{id}")]
        public async Task<Live> GetByID(int id)
        {
            try
            {
                return await _live.GetByID(id);
            }
            catch (Exception ex)
            {
                return (Live)Enumerable.Empty<Live>();
            }
        }
    }
}
