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
    public class InvoicesController : ControllerBase
    {
            private readonly IInvoiceServices _invoiceServices;
            public InvoicesController(IInvoiceServices invoiceServices)
            {
            _invoiceServices = invoiceServices;

            }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<Invoice> invoices = _invoiceServices.GetAll();
                return Ok(invoices);
            }
            catch (Exception)
            {
                return Content("ops!");
            }
        }


        [HttpGet("{uid}")]
        public ActionResult<Invoice> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var invoice = _invoiceServices.GetByUid(uid);
                if (invoice == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(invoice); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }
       
       [HttpPost]
        public ActionResult Create(InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var invoice = new Invoice();

                invoice.InvoiceDate = invoiceDto.InvoiceDate;
                invoice.TotalAmount = invoiceDto.TotalAmount;
                invoice.IsPaid = invoiceDto.IsPaid;
                invoice.AppointmentId = invoiceDto.AppointmentId;
                invoice.PaymentId = invoiceDto.PaymentId;
                invoice.ClientId = invoiceDto.ClientId;

                var isCreated = _invoiceServices.Create(invoice);
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
        public IActionResult Update(string uid, [FromBody] InvoiceUpdateDto invoice)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (invoice == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _invoiceServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newInvoice = new Invoice
            {
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid,
                AppointmentId = invoice.AppointmentId,
                PaymentId = invoice.PaymentId,
                ClientId = invoice.ClientId,
            };


            _invoiceServices.Update(uid, newInvoice);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/categories/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _invoiceServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _invoiceServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}
