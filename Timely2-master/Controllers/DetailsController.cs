using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely.Controllers
{
    [Session]
    public class DetailsController : Controller
    {
        //private readonly IRepository<Detail> _repositoryDetail;
        //private readonly IRepository<Appointment> _repositoryAppointment;

        //public DetailsController(
        //     IRepository<Detail> repository,
        //     IRepository<Appointment> repositoryAppointment)

        //{
        //    _repositoryDetail = repository;
        //    _repositoryAppointment = repositoryAppointment;
        //}
        //public class DetailsController : Controller
        //{
        //    private readonly ApplicationDbContext _context;

        //    public DetailsController(ApplicationDbContext context)
        //    {
        //        _context = context;
        //    }
        //private readonly IUnitOfWork _unitOfWork;
        //publicDetailsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IDetailServices _detailServices;
        private readonly IAppointmentServices _appointmentServices;
        public DetailsController(IDetailServices detailServices, 
            IAppointmentServices appointmentServices)

        {

            _detailServices = detailServices;
            _appointmentServices = appointmentServices;
        }
        private void createList()
        {
            //IEnumerable<Appointment> appointment = _context.Appointments.ToList();
            //SelectList selectListItems = new SelectList(appointment, "Id", "Title");
            //ViewBag.Appointments = selectListItems;
            //IEnumerable<Appointment> appointments = _repositoryAppointment.GetAll();
            //SelectList selectListItems = new SelectList(appointments, "Id", "Title");
            //ViewBag.Appointments = selectListItems;
            IEnumerable<Appointment> appointments = _appointmentServices.GetAll();
            ViewBag.Appointments = new SelectList(appointments, "Id", "Title");
        }
        [HttpGet]
        public IActionResult Index()
        {
       
                //IEnumerable<Detail> detail = _context.Details.Include(c => c.Appointment).ToList();
                //  IEnumerable<Detail> details = _repositoryDetail.GetAll(
                //  A => A.Appointment);
                IEnumerable<Detail> details = _detailServices.GetAll();

                return View(details);
         }
        // create 
        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Detail detail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    createList();
                    return View(detail);
                }
                //_context.Details.Add(detail);
                //_context.SaveChanges();
               // _repositoryDetail.Add(detail);
               _detailServices.Create(detail);
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
            //var detail = _context.Details.Find(Id);
            // var detel = _repositoryDetail.GetByUid(Uid);
            var detel = _detailServices.GetByUid(Uid);
            createList();
            return View(detel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Detail detail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    createList();
                    return View(detail);
                }
                //var detail = _context.Details.AsNoTracking().FirstOrDefault(a => a.Uid == detail.Uid);
                //var details = _repositoryDetail.GetByUid(Uid);
                //details.Name = detail.Name;
                //details.Description = detail.Description;
                // _repositoryDetail.Update(detail);
                _detailServices.Update(detail.Uid, detail);
                //_context.Details.Update(detail);
                //_context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception )
            {     
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني 565455252545");
            }
        }
        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            //var detail = _context.Details.Find(Id);
           // var detel = _repositoryDetail.GetByUid(Uid);
            var detel = _detailServices.GetByUid(Uid);
            createList();
            return View(detel);
        }
        [HttpGet]
        public IActionResult ConfirmDelete(string Uid)
        {
            var detel = _detailServices.GetByUid(Uid);
            if (detel == null)
            {
                return NotFound();
            }

            createList();
            return View(detel);
        
        // _repositoryDetail.Delete(detel.Id);
        //var detele = _context.Deletes.FirstOrDefault(d => d.Uid == delete.Uid);
        //_context.Details.Remove(detail);
        // _context.SaveChanges();
        }
    }
}