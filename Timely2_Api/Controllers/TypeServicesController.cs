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
    public class TypeServicesController : ControllerBase
    {
        private readonly ITypeServiceServices _typeServiceServices;
        public TypeServicesController(ITypeServiceServices typeServiceServices
            )
        {
            _typeServiceServices = typeServiceServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
                IEnumerable<TypeService> typeService = _typeServiceServices.GetAll();

             
                return Ok(typeService);
        }
        [HttpGet("{uid}")]
        public ActionResult<Appointment> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var typeService = _typeServiceServices.GetByUid(uid);
                if (typeService == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(typeService); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(TypeServiceDto typeServiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var typeService = new TypeService();

                typeService.Name = typeServiceDto.Name;
                typeService.Description = typeServiceDto.Description;


                var isCreated = _typeServiceServices.Create(typeService);
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
        public IActionResult Update(string uid, [FromBody] TypeServiceUpdateDto typeService)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (typeService == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _typeServiceServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newTypeService = new TypeService
            {

                   Uid = typeService.Uid,
                   Name = typeService.Name,
                   Description = typeService.Description
            };


            _typeServiceServices.Update(uid, newTypeService);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/_typeService/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _typeServiceServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _typeServiceServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }


}

