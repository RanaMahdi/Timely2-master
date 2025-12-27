using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Controllers
{
    [Session]
    public class InvoicesController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public InvoicesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IRepository<Invoice> _repositoryInvoice;
        //private readonly IRepository<Appointment> _repositoryAppointment;
        //private readonly IRepository<Client> _repositoryClient;
        //private readonly IRepository<Payment> _repositoryPayment;

        //public InvoicesController(
        //    IRepository<Invoice> repository,
        //    IRepository<Appointment> repositoryAppointment,
        //    IRepository<Client> repositoryClient,
        //    IRepository<Payment> repositoryPayment)
        //{
        //    _repositoryInvoice = repository;
        //    _repositoryAppointment = repositoryAppointment;
        //    _repositoryClient = repositoryClient;
        //    _repositoryPayment = repositoryPayment;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public InvoicesController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IInvoiceServices _invoiceServices;
        private readonly IAppointmentServices _appointmentServices;
        private readonly IClientServices _clientServices;
        private readonly IPaymentServices _paymentServices;


        public InvoicesController(
            IInvoiceServices invoiceServices,
            IAppointmentServices appointmentServices,
            IClientServices clientServices,
            IPaymentServices paymentServices)
        {
            _invoiceServices = invoiceServices;
            _appointmentServices = appointmentServices;
            _clientServices = clientServices;
            _paymentServices = paymentServices;
        }
        private void createList()
        {
            // IEnumerable<Appointment> appointments = _repositoryAppointment.GetAll();
            IEnumerable<Appointment> appointments = _appointmentServices.GetAll();
            SelectList selectListItems = new SelectList(appointments, "Id", "Title");
            ViewBag.Appointments = selectListItems;

            // IEnumerable<Client> clients = _repositoryClient.GetAll();
            IEnumerable<Client> clients = _clientServices.GetAll();
            SelectList selectListItems1 = new SelectList(clients, "Id", "Name");
            ViewBag.Clients = selectListItems1;

            // IEnumerable<Payment> payments = _repositoryPayment.GetAll();
            IEnumerable<Payment> payments = _paymentServices.GetAll();
            SelectList selectListItems2 = new SelectList(payments, "Id", "PaymentMethod");
            ViewBag.Payments = selectListItems2;

            //IEnumerable<Appointment> appointments = _context.Appointments.ToList();
            //SelectList selectListItems = new SelectList(appointments, "Id", "Title");
            //ViewBag.Appointment = selectListItems;

            //IEnumerable<Client> clients = _context.Clients.ToList();
            //SelectList selectListItems1 = new SelectList(clients, "Id", "Name");
            //ViewBag.Clients = selectListItems1;

            //IEnumerable<Payment> payments = _context.Payments.ToList();
            //SelectList selectListItems2 = new SelectList(payments, "Id", "Amount");
            //ViewBag.PaymentMethods = new SelectList(new[] { "Cash", "Card", "Bank Transfer" });
        }
        // Displays a list of all Invoice
        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<Appointment> appointment = _context.Appointments.Include(c => c.Client).ToList();

            //  IEnumerable<Invoice> invoices = _repositoryInvoice.GetAll(
            IEnumerable<Invoice> invoices = _invoiceServices.GetAll();
                     //A => A.Appointment,
                     //C => C.Client,
                     //P => P.Payment);
                //var invoices = _context.Invoices
                //    .Include(A => A.Appointment)
                //    .Include(C => C.Client)
                //    .Include(P => P.Payment)
                //    .ToList();

                return View(invoices);
            }
           
        // Create 

        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(invoice);
                }
                //_context.Invoices.Add(invoice);
                //_context.SaveChanges();
               // _repositoryInvoice.Add(invoice);
                _invoiceServices.Create(invoice);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("حدث خطأ في النظام لطفا تواصل مع الدعم الفني 056789876");
            }
        }
        //Edit

        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            createList();
            //var invo = _context.Invoices.Find(Id);
            //var invo = _repositoryInvoice.GetByUid(Uid);
            var invo = _invoiceServices.GetByUid(Uid);
            return View(invo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice invoice,  string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(invoice);
                }
                //var appoint = _context.Invoices.AsNoTracking().FirstOrDefault(a => a.Uid == invoice.Uid);
               // var invo = _repositoryInvoice.GetByUid(Uid);
                var invo = _invoiceServices.GetByUid(Uid);
                if (invo == null)
                {
                    return NotFound();
                }
                invo.InvoiceDate = invoice.InvoiceDate;
                invo.TotalAmount = invoice.TotalAmount;
                invo.IsPaid = invoice.IsPaid;
                invo.AppointmentId = invoice.AppointmentId;
                invo.ClientId = invoice.ClientId;
                invo.PaymentId = invoice.PaymentId;
                _invoiceServices.Update(Uid, invo);

                //  _repositoryInvoice.Update(invo);
                //_context.Invoices.Update(invoice);
                //_context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني");
            }
        }
        //Delete
        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            createList();
            var invoic = _invoiceServices.GetByUid(Uid);
           // var invoic = _repositoryInvoice.GetByUid(Uid);
            //var invoic = _context.Invoices.Find(Id);
            return View(invoic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Invoice invoice,string Uid)
        {
           // var invco = _repositoryInvoice.GetByUid(Uid);
            var invco = _invoiceServices.GetByUid(Uid);
            if (invco == null)
            {
                return NotFound();
            }
            _invoiceServices.DeleteByUid(Uid);
            //_repositoryInvoice.Delete(invco.Id);
            //_context.Invoices.Remove(invoice);
            //_context.SaveChanges();
            return RedirectToAction("Index"); 
        }
    }
}
