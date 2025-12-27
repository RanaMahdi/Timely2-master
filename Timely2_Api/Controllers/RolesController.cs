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
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleServices.GetAll();
            return Ok(roles);
        }

        [HttpGet("{uid}")]
        public ActionResult<Role> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var role = _roleServices.GetByUid(uid);
                if (role == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(role); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(RoleDto roleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var role = new Role();

                role.Name = roleDto.Name;
                role.Permission = roleDto.Permission;

                var isCreated = _roleServices.Create(role);
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
        public IActionResult Update(string uid, [FromBody] RoleUpdateDto role)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (role == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _roleServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newRole = new Role
            {

                Uid = role.Uid,
                Name = role.Name,
                Permission = role.Permission,
            };


            _roleServices.Update(uid, newRole);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _roleServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _roleServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}
