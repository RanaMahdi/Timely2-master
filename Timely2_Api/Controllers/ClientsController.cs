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
    public class ClientsController : ControllerBase
    {
        private readonly IClientServices _clientServices;
        public ClientsController(IClientServices clientServices)
        {
            _clientServices = clientServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Client> clients = _clientServices.GetAll();

                return Ok(clients);
            }
            catch (Exception)
            {

                return Content("حدث خطا  غير متوقع يرجي مراجهة الدعم الفني:0565455252545");

            }
        }


        [HttpGet("{uid}")]
        public ActionResult<Client> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var client = _clientServices.GetByUid(uid);
                if (client == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(client); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(ClientDto clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var client = new Client();

                client.Name = clientDto.Name;
                client.Phone = clientDto.Phone;
                client.Email = clientDto.Email;
                client.Password = clientDto.Password;
                client.Gender = clientDto.Gender;
                client.NationalityId = clientDto.NationalityId;


                var isCreated = _clientServices.Create(client);
                if (isCreated)
                    //  return CreatedAtAction(nameof(GetByUid), new { uid = client.Uid }, client); // 201

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
        public IActionResult Update(string uid, [FromBody] ClientUpdateDto client)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (client == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _clientServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newClient = new Client
            {
                Name = client.Name,
                Phone = client.Phone,
                Email = client.Email,
                Password = client.Password,
                Gender = client.Gender,
                NationalityId = client.NationalityId,

            };






            _clientServices.Update(uid, newClient);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _clientServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _clientServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }




    }
}