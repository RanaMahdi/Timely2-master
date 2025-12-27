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
    public class ServicesController : ControllerBase
    {
        private readonly IServicesServices _servicesServices;
        public ServicesController(IServicesServices servicesServices)
        {
            _servicesServices = servicesServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Services> services = _servicesServices.GetAll();
                return Ok(services);
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

                var services = _servicesServices.GetByUid(uid);
                if (services == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(services); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(ServicesDto servicesDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var services = new Services();

                services.Name = servicesDto.Name;
                services.Price = servicesDto.Price;
                services.IsActive = servicesDto.IsActive;
                services.TypeServiceId = servicesDto.TypeServiceId;
                services.DepartmentId = servicesDto.DepartmentId;
             

                var isCreated = _servicesServices.Create(services);
                if (isCreated)

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
        public IActionResult Update(string uid, [FromBody] ServicesUpdateDto services)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (services == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _servicesServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newServices = new Services
            {

                Uid = services.Uid,
                Name = services.Name,
                Price = services.Price,
                IsActive = services.IsActive,
                TypeServiceId = services.TypeServiceId,
                DepartmentId = services.DepartmentId
            };

            _servicesServices.Update(uid, newServices);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _servicesServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _servicesServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}