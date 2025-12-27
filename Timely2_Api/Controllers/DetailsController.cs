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
    public class DetailsController : ControllerBase
    {
            private readonly IDetailServices  _detailServices;
            public DetailsController(IDetailServices detailServices)
            {
               _detailServices = detailServices;
            }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Detail> details = _detailServices.GetAll();

            return Ok(details);
        }
        [HttpGet("{uid}")]
        public ActionResult<Detail> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var detail = _detailServices.GetByUid(uid);
                if (detail == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(detail); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(DetailDto detailDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var detail = new Detail();

                detail.Name = detailDto.Name;
                detail.Description = detailDto.Description;
                detail.CreatedDate = detailDto.CreatedDate;
                detail.AppointmentId = detailDto.AppointmentId;
             


                var isCreated = _detailServices.Create(detail);
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
        public IActionResult Update(string uid, [FromBody] DetailUpdateDto detail)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (detail == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _detailServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newDetail = new Detail
            {
                Name = detail.Name,
                Description = detail.Description,
                CreatedDate = detail.CreatedDate,
                AppointmentId = detail.AppointmentId,
            };






            _detailServices.Update(uid, newDetail);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _detailServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _detailServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}
