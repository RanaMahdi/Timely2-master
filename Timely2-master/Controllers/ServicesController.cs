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
    public class ServicesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //public ServicesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //private readonly IRepository<Services> _repositoryService;
        //private readonly IRepository<TypeService> _repositoryTypeService;
        //private readonly IRepository<Department> _repositoryDepartment;

        //public ServicesController(
        //  IRepository<Services> repository,
        //  IRepository<TypeService> repositoryTypeService,
        //  IRepository<Department> repositoryDepartment)
        //{
        //    _repositoryService = repository;
        //    _repositoryTypeService = repositoryTypeService;
        //    _repositoryDepartment = repositoryDepartment;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public ServicesController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IServicesServices _servicesServices;
        private readonly ITypeServiceServices _typeServiceServices;
        private readonly IDepartmentServices _departmentServices;

        public ServicesController(
            IServicesServices servicesServices,
             ITypeServiceServices typeServiceServices,
             IDepartmentServices departmentServices
            )
        {
            _servicesServices = servicesServices;
            _typeServiceServices = typeServiceServices;
            _departmentServices = departmentServices;
        }
        private void createList()
        {
            //IEnumerable<TypeService> typeServices = _context.TypeServices.ToList();
            //SelectList selectListItems = new SelectList(typeServices, "Id", "Name");
            //ViewBag.TypeServices = selectListItems;

            //IEnumerable<Department> department = _context.Departments.ToList();
            //SelectList selectListItems1 = new SelectList(department, "Id", "Name");
            //ViewBag.Departments = selectListItems1;

          //  IEnumerable<TypeService> typeService = _repositoryTypeService.GetAll();
            IEnumerable<TypeService> typeService = _typeServiceServices.GetAll();
            SelectList selectListItems = new SelectList(typeService, "Id", "Name");
            ViewBag.TypeServices = selectListItems;

           // IEnumerable<Department> departments = _repositoryDepartment.GetAll();
            IEnumerable<Department> departments = _departmentServices.GetAll();
            SelectList selectListItems1 = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = selectListItems1;


            ViewBag.Active = new List<SelectListItem>
            {
                new SelectListItem {Text = "Yes", Value ="True"},
                new SelectListItem {Text = "No", Value ="False"},

            };
        }
        // Displays a list of all services
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // IEnumerable<Services> services = _repositoryService.GetAll(
                IEnumerable<Services> services = _servicesServices.GetAll();
                 //t => t.TypeService,
                 //d => d.Department);
            //foreach (var service in services)
            //{

            //    service.TypeService = _repositoryTypeService.GetAll()
            //        .FirstOrDefault(t => t.Id == service.TypeServiceId);
            //}

            return View(services);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }
        // create a new service

        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Services service)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(service);
                }
                //_context.Services.Add(service);
                //_context.SaveChanges();
                //_repositoryService.Add(service);
                _servicesServices.Create(service);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("حدث خطأ في النظام لطفا تواصل مع الدعم الفني 056789876");
            }
        }

        [HttpGet]
        public IActionResult Edit(string Uid)
        {
           // var service = _repositoryService.GetByUid(Uid);
            var service = _servicesServices.GetByUid(Uid);
            createList();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Services service, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(service);
                }
                //var appoint = _context.Service.AsNoTracking().FirstOrDefault(a => a.Uid == service.Uid);
                //var serv = _repositoryService.GetByUid(Uid);
                var serv = _servicesServices.GetByUid(Uid);
                if (serv == null)
                {
                    return NotFound();
                }
                serv.Price = service.Price;
                serv.IsActive = service.IsActive;

                //_context.Appointments.Update(appoint);
                //_context.SaveChanges();
               // _repositoryService.Update(serv);
                _servicesServices.Update(Uid, serv);
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
            //var appoint = _context.Services.FirstOrDefault(a => a.Uid == Uid);
           // var serv = _repositoryService.GetByUid(Uid);
            var serv = _servicesServices.GetByUid(Uid);

                createList();
                return View(serv);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete(Services service,string Uid)
            {
                // var serv = _repositoryService.GetByUid(Uid);
                var serv = _servicesServices.GetByUid(service.Uid);
                if (serv == null)
                {
                    return NotFound();
                }
            _typeServiceServices.DeleteByUid(Uid);
                return RedirectToAction("Index");
            }
        }
    }












