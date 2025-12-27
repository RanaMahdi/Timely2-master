using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely.Controllers
{
    [Session]
    public class PaymentsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //public PaymentsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly IRepository<Payment> _repositoryPayment;
        //private readonly IRepository<Client> _repositoryClient;
        //private readonly IRepository<Appointment> _repositoryAppointment;

        //public PaymentsController(
        //    IRepository<Payment> repository,
        //    IRepository<Client> repositoryClient,
        //    IRepository<Appointment> repositoryAppointment)
        //{
        //    _repositoryPayment = repository;
        //    _repositoryClient = repositoryClient;
        //    _repositoryAppointment = repositoryAppointment;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public PaymentsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly IPaymentServices _paymentServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IServicesServices _servicesServices;
        private readonly INationalityServices _nationalityServices;
        private readonly IClientServices _clientServices;
        private readonly IAppointmentServices _appointmentServices;

        public PaymentsController(
            IPaymentServices paymentServices,
            IDepartmentServices departmentServices,
            IServicesServices servicesServices,
            INationalityServices nationalityServices,
            IClientServices clientService,
            IAppointmentServices appointmentServices)
        {
            _paymentServices = paymentServices;
            _departmentServices = departmentServices;
            _servicesServices = servicesServices;
            _nationalityServices = nationalityServices;
            _clientServices = clientService;
            _appointmentServices = appointmentServices;
        }

        private void createList()
        {
            //IEnumerable<Client> clients = _context.Clients.ToList();
            //SelectList selectListItems = new SelectList(clients, "Id", "Name");
            //ViewBag.Clients = selectListItems;

            //IEnumerable<Appointment> appointments = _context.Appointments.ToList();
            //SelectList selectListItems1 = new SelectList(appointments, "Id", "Title");
            //ViewBag.Appointment = selectListItems1;

            IEnumerable<Client> clients = _clientServices.GetAll();
            SelectList selectListItems = new SelectList(clients, "Id", "Name");
            ViewBag.Clients = selectListItems;

            IEnumerable<Appointment> appointments = _appointmentServices.GetAll();
            SelectList selectListItems1 = new SelectList(appointments, "Id", "Title");
            ViewBag.Appointments = selectListItems1;
            ViewBag.PaymentMethods = new SelectList(new[] { "Cash", "Card", "Bank Transfer" });
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Payment> payments = _paymentServices.GetAll();
                //IEnumerable<Payment> payments = _repositoryPayment.GetAll(
                //C => C.Client,
                //A => A.Appointment);
                //var payment = _context.Payments
                //.Include(c => c.Client)
                //.Include(c => c.Appointment)
                //.ToList();

                return View(payments);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        // Displays a list of all payments
        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment payment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(payment);
                }
                //_context.Payments.Add(payment);
                //_context.SaveChanges();
                //_repositoryPayment.Add(payment);
                //_unitOfWork._paymentRepo.Add(payment);
                _paymentServices.Create(payment);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            //var payment = _context.Payments.Find(Id); creatLest();
            //var payment = _repositoryPayment.GetByUid(Uid);
            var payment = _paymentServices.GetByUid(Uid);
            createList();
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Payment payment, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(payment);
                }
                //_context.Payments.Update(payment);
                //_context.SaveChanges();
                //var appoint = _context.Appointments.AsNoTracking().FirstOrDefault(a => a.Uid == appointment.Uid);
                //var pay = _repositoryPayment.GetByUid(Uid);
                //_repositoryPayment.Update(pay);
                //_unitOfWork._paymentRepo.Add(payment);

                var pay = _paymentServices.GetByUid(Uid);
                if (pay == null)
                {
                    return NotFound();
                }
                pay.Amount = payment.Amount;
                pay.PaymentDate = payment.PaymentDate;
                pay.PaymentMethod = payment.PaymentMethod;
                pay.AppointmentId = payment.AppointmentId;
                pay.ClientId = payment.ClientId;
                _paymentServices.Update(Uid, pay);

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني");
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            //var payment = _context.Payments.Find(Id);
            //var pay = _repositoryPayment.GetByUid(Uid);
            //var pay = _unitOfWork._paymanttRepo.GetByUId(Uid);
            var pay = _paymentServices.GetByUid(Uid);
            createList();
            return View(pay);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Payment payment, string Uid)
        {
            var pay = _paymentServices.GetByUid(Uid);
            if (pay == null)
            {
                return NotFound();
            }
            _paymentServices.DeleteByUid(Uid);
            //var pay = _repositoryPayment.GetByUid(Uid);
            //_repositoryPayment.Delete(pay.Id);
            //_context.Payments.Remove(payment);
            //_context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}