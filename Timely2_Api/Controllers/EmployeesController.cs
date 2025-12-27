using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timely.Dtos;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely2_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServices _empService;
        public EmployeesController(IEmployeeServices empService)
        {
            _empService = empService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAll()
        {
            try
            {
                IEnumerable<Employee> Emp = _empService.GetAll();
                return Ok(Emp);
            }
            catch (Exception)
            {
                return Content(" error ");
            }
        }

        [HttpGet("{uid}")]
        public ActionResult<Employee> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var Emps = _empService.GetByUid(uid);
                if (Emps == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(Emps); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }

        }


        [HttpPost]
        public ActionResult Create(EmplDot emplDot)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var emp = new Employee();
                emp.Name = emplDot.Name;
                emp.Phone = emplDot.Phone;
                emp.Password = emplDot.Password;
                emp.Email = emplDot.Email;
                emp.DepartmentId = emplDot.DepartmentId;
                emp.DepartmentWorkingId = emplDot.DepartmentWorkingId;
                emp.JobId = emplDot.JobId;
                emp.NationalityId = emplDot.NationalityId;
                var isCreated = _empService.Create(emp);
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
        public IActionResult Update(string uid, [FromBody] EmplUpdateDto employee)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (employee == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _empService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var NewEmp = new Employee
            {
                Uid = employee.Uid,
                Name = employee.Name,
                Phone = employee.Phone,
                Password = employee.Password,
                Email = employee.Email,
            };
            _empService.Update(uid, NewEmp);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _empService.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _empService.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }


    }

}
