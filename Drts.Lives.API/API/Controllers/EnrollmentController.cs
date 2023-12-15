using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Drts.Lives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController(IEnrollmentApplication enrollment,
                                      IPersonApplication person,
                                      ILiveApplication live) : ControllerBase
    {
        private readonly IEnrollmentApplication _enrollment = enrollment;
        private readonly IPersonApplication _person = person;
        private readonly ILiveApplication _live = live;

        [HttpPost("/api/enrollment")]
        public async Task<IActionResult> Create([FromBody] EnrollmentDTO entityDTO)
        {
            try
            {
                var live = await _live.GetByID(entityDTO.live_id);
                if (live == null) return NotFound(new {ErrorMessage = $"Live not found. (id {entityDTO.live_id})" });

                var subscribed = await _person.GetByID(entityDTO.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound(new {ErrorMessage = $"Subscribed not found. (id {entityDTO.person_registered_id})" });

                Enrollment enrollment = new()
                {
                    live_id = entityDTO.live_id,
                    person_registered_id = entityDTO.person_registered_id,
                    value = entityDTO.value,
                    expiration_date = entityDTO.expiration_date,
                    payment_status = entityDTO.payment_status
                };

                if (await _enrollment.IsDuplicat(enrollment)) return BadRequest(new { ErrorMessage = $"The subscriber is already registered and is live. (id live {entityDTO.live_id}, id subscribed {entityDTO.person_registered_id})" }); 

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
                if (enrollment == null) return NotFound(new {ErrorMessage = $"Enrollment not found. (id {id})"});

                Live live = await _live.GetByID(entityDTO.live_id);
                if (live == null) return NotFound( new {ErrorMessage = $"Live not found. (id {entityDTO.live_id})" });

                Person subscribed = await _person.GetByID(entityDTO.person_registered_id, PersonTypeEnum.subscribed);
                if (subscribed == null) return NotFound(new {ErrorMessage = $"Subscribed not found. (id {entityDTO.person_registered_id})" });

                Enrollment enrollmentNew = new()
                {
                    id = id,
                    live_id = entityDTO.live_id,
                    person_registered_id = entityDTO.person_registered_id,
                    value = entityDTO.value,
                    expiration_date = entityDTO.expiration_date,
                    payment_status = entityDTO.payment_status
                };

                if (await _enrollment.IsDuplicat(enrollment)) return BadRequest(new { ErrorMessage = $"The subscriber is already registered and is live. (id live {entityDTO.live_id}, id subscribed {entityDTO.person_registered_id})" });

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
                if (enrollment == null) return NotFound(new {ErrorMessage = $"Enrollment not found. (id {id})" });

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
            catch (Exception)
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
            catch (Exception)
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
                if (enrollment == null) return NotFound(new { ErrorMessage = $"Enrollment not found. (id {entity.enrollment_id})" });
                if (enrollment.expiration_date < DateTime.Now) return BadRequest(new { ErrorMessage = "Enrollment has expired" });
                if (enrollment.payment_status == PaymentStatusEnum.processed) return BadRequest(new {ErrorMessage = "Enrollment is already paid" });

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
