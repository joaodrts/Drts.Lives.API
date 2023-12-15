using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivesController(ILiveApplication live,
                                 IPersonApplication person) : ControllerBase
    {
        private readonly ILiveApplication _live = live;
        private readonly IPersonApplication _person = person;

        [HttpPost("/api/lives")]
        public async Task<IActionResult> Create([FromBody] LiveDTO liveDTO)
        {
            try
            {
                var instructor = await _person.GetByID(liveDTO.instructor_id, PersonTypeEnum.instructor);
                if (instructor == null) return NotFound(new {ErrorMessage = $"Instructor not found. (id {liveDTO.instructor_id})" });

                Live live = new()
                {
                    title = liveDTO.title,
                    description = liveDTO.description,
                    instructor_id = liveDTO.instructor_id,
                    start_date = liveDTO.start_date,
                    duration_in_minutes = liveDTO.duration_in_minutes,
                };
                await _live.Add(live);

                return Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/lives/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] LiveDTO liveDTO)
        {
            try
            {
                var instructor = await _person.GetByID(liveDTO.instructor_id, PersonTypeEnum.instructor);
                if (instructor == null) return NotFound(new { ErrorMessage = $"Instructor not found. (id {liveDTO.instructor_id})" });

                Live liveOld = await _live.GetByID(id);
                if (liveOld == null) return NotFound(new {ErrorMessage = $"Live not found. (id {id})"});

                Live live = new()
                {
                    id = id,
                    title = liveDTO.title,
                    description = liveDTO.description,
                    instructor_id = liveDTO.instructor_id,
                    start_date = liveDTO.start_date,
                    duration_in_minutes = liveDTO.duration_in_minutes,
                };
                await _live.Update(live);

                return Ok("Updated successfully");
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
                if (live == null) return NotFound(new {ErrorMessage = $"Live not found. (id {id})"});

                await _live.Remove(live);

                return Ok("Successfully removed");
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
            catch (Exception)
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
            catch (Exception)
            {
                return (Live)Enumerable.Empty<Live>();
            }
        }
    }
}
