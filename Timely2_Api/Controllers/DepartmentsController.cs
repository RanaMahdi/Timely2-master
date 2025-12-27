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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentsController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var departments = _departmentServices.GetAll()
                    .Select(d => new {
                        d.Id,
                        d.Name,
                        d.Description,
                        d.ImageUrl
                    });
                return Ok(departments);
            }

            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }


        [HttpGet("{uid}")]
        public ActionResult<Department> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var appointment = _departmentServices.GetByUid(uid);
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
        public ActionResult Create(DepartmentDto departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var department = new Department();

                department.Name = departmentDto.Name;
                department.Description = departmentDto.Description;
               

                var isCreated = _departmentServices.Create(department);
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
        public IActionResult Update(string uid, [FromBody] DepartmentUpdateDto department)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (department == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _departmentServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newDepartment = new Department
            {
                Name = department.Name,
                Description = department.Description,
            };

            _departmentServices.Update(uid, newDepartment);
            return Ok("تم تحديث الفئة بنجاح");
        }



        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _departmentServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _departmentServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }





    }
}