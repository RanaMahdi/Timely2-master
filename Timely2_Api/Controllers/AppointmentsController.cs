using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timely.Dtos;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely2_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentServices _appointmentService;
        public AppointmentsController(IAppointmentServices appointmentServices)
        {
            _appointmentService = appointmentServices;

        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Appointment> appointments = _appointmentService.GetAll();
                return Ok(appointments);
            }
            catch (Exception)
            {
                return Content("ops!");
            }
        }

        [HttpGet("{uid}")]
        public ActionResult<Appointment> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var appointment = _appointmentService.GetByUid(uid);
                if (appointment == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(appointment); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(AppointmentDto appointmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appointment = new Appointment();

                appointment.Title = appointmentDto.Title;
                appointment.Description = appointmentDto.Description;
                appointment.AppointmentDateTime = appointmentDto.AppointmentDateTime;
                appointment.Status = appointmentDto.Status;
                appointment.UpdatedAt = appointmentDto.UpdatedAt;
                appointment.ClientId = appointmentDto.ClientId;
                appointment.EmployeeId = appointmentDto.EmployeeId;
                   

        var isCreated = _appointmentService.Create(appointment);
                if (isCreated)
                    //  return CreatedAtAction(nameof(GetByUid), new { uid = category.Uid }, category); // 201

                    return Ok("تم انشاء الفئه بنجاح"); // 200

                return BadRequest("فشل انشاء الفئه"); // 400

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }
        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] AppointmentUpdateDto appointment)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (appointment == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _appointmentService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newAppointment = new Appointment
            {

                Uid = appointment.Uid,
                Title = appointment.Title,
                Description = appointment.Description,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.Status,
                UpdatedAt = appointment.UpdatedAt,
                ClientId = appointment.ClientId,
                EmployeeId = appointment.EmployeeId
            };






            _appointmentService.Update(uid, newAppointment);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _appointmentService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _appointmentService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}