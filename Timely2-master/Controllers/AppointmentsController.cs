using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
 using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Repositories;

namespace Timely.Controllers
{
    [Session]
    public class AppointmentsController : Controller
    {
        //private readonly IEmployeeServices _employeeServices;
        private readonly IAppointmentServices _appointmentService;
        public AppointmentsController(IAppointmentServices appointmentServices)
        {
            _appointmentService = appointmentServices;
          
        }

        //private readonly ApplicationDbContext _context;
        //public AppointmentsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public AppointmentsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private void createList()
        {
            //IEnumerable<Client> clients = _context.Clients.ToList();
            //SelectList selectListItems = new SelectList(clients, "Id", "Name");
            //ViewBag.Clients = selectListItems;

            //IEnumerable<Employee> employees = _context.Employees.ToList();
            //SelectList selectListItems1 = new SelectList(employees, "Id", "Name");
            //ViewBag.Employees = selectListItems1;

            //IEnumerable<Client> clients = _repositoryClient.GetAll();
            //SelectList selectListItems = new SelectList(clients, "Id", "Name");
            //IEnumerable<Department> departments = _employeeServices.GetDepartments();

            //IEnumerable<Employee> employees = _repositoryEmployees.GetAll();
            //SelectList selectListItems1 = new SelectList(employees, "Id", "Name");
            //IEnumerable<Employee> employees = _repositoryEmployees.GetAll();

            //IEnumerable<Client> clients = _appointmentService.GetAllClients();
            //SelectList selectListItems = new SelectList(clients, "Id", "Name");
            //ViewBag.Clients = selectListItems;

            //IEnumerable<Employee> employees = _appointmentService.GetAllEmployees();
            //SelectList selectListItems1 = new SelectList(employees, "Id", "Name");
            //ViewBag.Employees = selectListItems1;
        }

        // Displays a list of all Appointment
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //IEnumerable<Appointment> appointment = _context.Appointments.Include(c => c.Client).ToList();
                // IEnumerable<Appointment> appointments = _appointmentsRepo.GetAll();
                // _repositoryAppointment.GetAll(E => E.Employee, C => C.Client);

                //foreach (var item in appointments)
                //{
                //    item.Uid = Guid.NewGuid().ToString();
                //    _context.Appointments.Update(item);
                //    _context.SaveChanges();
                //}
                IEnumerable<Appointment> appointments = _appointmentService.GetAll();
                return View(appointments);
            }
            catch (Exception)
            {
                return Content("ops!");
            }
        }

        // Create a new Appointment
        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(appointment);
                }

                //_context.Appointments.Add(appointment);
                //_context.SaveChanges();
               // _appointmentsRepo.Add(appointment);
                _appointmentService.Create(appointment);
                //_repositoryAppointment.Add(appointment);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        // Action Edit
        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            //var appoint = _context.Appointments.FirstOrDefault(e => e.Uid == Uid);
            //var appointment = _appointmentsRepo.GetByUId(Uid);
            //var appoint = _repositoryAppointment.GetByUId(Uid);
            var appointment = _appointmentService.GetByUid(Uid);

            createList();
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(appointment);
                }

                //var appoint = _context.Appointments.AsNoTracking().FirstOrDefault(a => a.Uid == appointment.Uid);
                //var appoint = _appointmentsRepo.GetByUId(Uid);
                //var appoint = _repositoryAppointment.GetByUId(Uid);
                var appoint = _appointmentService.GetByUid(Uid);
                if (appoint == null)
                {
                    return NotFound();
                }

                appoint.Title = appointment.Title;
                appoint.Description = appointment.Description;
                appoint.AppointmentDateTime = appointment.AppointmentDateTime;
                appoint.Status = appointment.Status;
                appoint.ClientId = appointment.ClientId;
                appoint.EmployeeId = appointment.EmployeeId;
                //appointment.UpdatedAt = DateTime.Now;

                //_context.Appointments.Update(appoint);
                //_context.SaveChanges();
                //_appointmentsRepo.Update(appoint);
                //_repositoryAppointment.Update(appoint);
                _appointmentService.Update(Uid, appointment);
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني");
            }
        }

        // Action Delete
        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            //var appoint = _context.Appointments.AsNoTracking().FirstOrDefault(a => a.Uid == Uid);
            // var appointment = _appointmentsRepo.GetByUId(Uid);
            //var appointment = _repositoryAppointment.GetByUId(Uid);
            var appoint = _appointmentService.GetByUid(Uid);
            createList();
            return View(appoint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Appointment appointment, string Uid)
        {
            //var appointmentToDelete = _appointmentsRepo.GetByUId(Uid);
            //var appoint = _repositoryAppointment.GetByUId(Uid);
            var appoint = _appointmentService.GetByUid(Uid);
            if (appoint == null)
            {
                return NotFound();
            }

            //_repositoryAppointment.Delete(appointment.Id);
            //_appointmentsRepo.Delete(appointment.Id);
            //var debt = _context.Departments.FirstOrDefault(d => d.Uid == department.Uid);
            //_context.Departments.Remove(debt);
            //_context.SaveChanges();
            _appointmentService.DeleteByUid(Uid); 
            return RedirectToAction("Index");
        }
    }
}