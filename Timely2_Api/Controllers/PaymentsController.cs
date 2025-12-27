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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        public PaymentsController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Payment> payments = _paymentServices.GetAll();
                return Ok(payments);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }
            [HttpGet("{uid}")]
            public ActionResult<Payment> GetByUid(string uid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(uid))
                        return BadRequest("Uid is required.");

                    var payment = _paymentServices.GetByUid(uid);
                    if (payment == null)
                        return NotFound("لا توجد فئه بهذا الرقم"); // 404

                    return Ok(payment); // 200

                }
                catch (Exception ex)
                {
                    var message = ex.Message.ToString();
                    return BadRequest($"{message}حدث خطا غير متوقع");
                }
            }
        [HttpPost]
        public ActionResult Create(PaymentDto paymentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var payment = new Payment();

                payment.Amount = paymentDto.Amount;
                payment.PaymentDate = paymentDto.PaymentDate;
                payment.PaymentMethod = paymentDto.PaymentMethod;
                payment.AppointmentId = paymentDto.AppointmentId;
                payment.ClientId = paymentDto.ClientId;
             
                var isCreated = _paymentServices.Create(payment);
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
        public IActionResult Update(string uid, [FromBody] PaymentUpdateDto payment)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (payment == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _paymentServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newPayment = new Payment
            {

                Uid = payment.Uid,
               Amount = payment.Amount,
               PaymentDate = payment.PaymentDate,
               PaymentMethod = payment.PaymentMethod,
               AppointmentId = payment.AppointmentId,
               ClientId = payment.ClientId,
            };
            _paymentServices.Update(uid, newPayment);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/payments/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _paymentServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _paymentServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}