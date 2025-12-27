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
    public class DepartmentWorkingsController : ControllerBase
    {
        private readonly IDepartWorkServices _departWorkServices;

        public DepartmentWorkingsController(IDepartWorkServices departWorkServices)
        {
            _departWorkServices = departWorkServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
             
                IEnumerable<DepartmentWorking> departmentWorkings = _departWorkServices.GetAll();
                return Ok(departmentWorkings);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }
        [HttpPost]
        public ActionResult Create(DepartmentWorkingDto departmentWorkingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var departWork = new DepartmentWorking();

                departWork.Name = departmentWorkingDto.Name;
                departWork.StartTime = departmentWorkingDto.StartTime;
                departWork.EndTime = departmentWorkingDto.EndTime;
                departWork.IsActive = departmentWorkingDto.IsActive;
                departWork.Day = departmentWorkingDto.Day;
                departWork.DepartmentId = departmentWorkingDto.DepartmentId;
                departWork.ServiceId = departmentWorkingDto.ServiceId;
              

                var isCreated = _departWorkServices.Create(departWork);
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
        public IActionResult Update(string uid, [FromBody] DepartmentWorkingUpdateDto departWork)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (departWork == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _departWorkServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newDepartWork = new DepartmentWorking
            {

               Name = departWork.Name,
               StartTime = departWork.StartTime,
               EndTime = departWork.EndTime,
               IsActive = departWork.IsActive,
               Day = departWork.Day,
               DepartmentId = departWork.DepartmentId,
               ServiceId = departWork.ServiceId,
            };

            _departWorkServices.Update(uid, newDepartWork);
            return Ok("تم تحديث الفئة بنجاح");
        }


        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _departWorkServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _departWorkServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }





    }
}