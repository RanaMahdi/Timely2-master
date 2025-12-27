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
    public class JobsController : ControllerBase
    {
        private readonly IJobServices _jobServices;
        public JobsController(IJobServices jobServices)
        {
            _jobServices = jobServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Job> jobs = _jobServices.GetAll();
            return Ok(jobs);

        }

        [HttpGet("{uid}")]
        public ActionResult<Job> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var job = _jobServices.GetByUid(uid);
                if (job == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(job); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(JobDto jobDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var job = new Job();

                job.Name = jobDto.Name;
                job.BaseSalary = jobDto.BaseSalary;
               


                var isCreated = _jobServices.Create(job);
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
        public IActionResult Update(string uid, [FromBody] JobUpdateDto job)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (job == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _jobServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newJob = new Job
            {
                Uid = job.Uid,
                Name = job.Name,
                BaseSalary = job.BaseSalary,

            };
            _jobServices.Update(uid, newJob);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _jobServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _jobServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}