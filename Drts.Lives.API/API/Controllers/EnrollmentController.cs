using Application.DTOs;
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
        public async Task<IActionResult> Create([FromBody] EnrollmentDTO entityDTO)
        {
            try
            {
                var live = await _live.GetByID(entityDTO.live_id);
                if (live == null) return NotFound("Live not found");

                var subscribed = await _person.GetByID(entityDTO.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound("Subscribed not found");

                Enrollment enrollment = new()
                {
                    live_id = entityDTO.live_id,
                    person_registered_id = entityDTO.person_registered_id,
                    value = entityDTO.value,
                    expiration_date = entityDTO.expiration_date,
                    payment_status = entityDTO.payment_status
                };

                await _enrollment.Add(enrollment);
                return Ok("Successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/enrollment/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EnrollmentDTO entityDTO)
        {
            try
            {
                Enrollment enrollment = await _enrollment.GetByID(id);
                if (enrollment == null) return NotFound();

                var live = await _live.GetByID(entityDTO.live_id);
                if (live == null) return NotFound("Live not found");

                var subscribed = await _person.GetByID(entityDTO.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound("Subscribed not found");

                Enrollment enrollmentNew = new()
                {
                    id = id,
                    live_id = entityDTO.live_id,
                    person_registered_id = entityDTO.person_registered_id,
                    value = entityDTO.value,
                    expiration_date = entityDTO.expiration_date,
                    payment_status = entityDTO.payment_status
                };
                await _enrollment.Update(enrollmentNew);

                return Ok("Updated successfully");

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

                return Ok("Successfully removed");
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

        [HttpPost("/api/enrollment/payment")]
        public async Task<IActionResult> Payment([FromBody] Payment entity)
        {
            try
            {
                Enrollment enrollment = await _enrollment.GetByID(entity.enrollment_id);
                if (enrollment == null) return NotFound("Enrollment not found");
                if (enrollment.expiration_date < DateTime.Now) return BadRequest("Enrollment has expired");
                if (enrollment.payment_status == PaymentStatusEnum.processed) return BadRequest("Enrollment is already paid");

                enrollment.payment_status = PaymentStatusEnum.processed;
                await _enrollment.Update(enrollment);

                return Ok("Paid successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
