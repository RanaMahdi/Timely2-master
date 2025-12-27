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
    public class DepartmentWorkingsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //public DepartmentWorkingsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IRepository<DepartmentWorking> _repositoryDepartmentWorking;
        //private readonly IRepository<Department> _repositoryDepartment;
        //private readonly IRepository<Services> _repositoryService;
        //private readonly IRepository<Nationality> _repositoryNationality;

        //public DepartmentWorkingsController(
        //  IRepository<DepartmentWorking> repository,
        //  IRepository<Department> repositoryClient,
        //  IRepository<Services> repositoryService)
        //{
        //    _repositoryDepartmentWorking = repository;
        //    _repositoryDepartment = repositoryClient;
        //    _repositoryService = repositoryService;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public DepartmentWorkingsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IDepartWorkServices _departWorkServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IServicesServices _servicesServices;
        private readonly INationalityServices _nationalityServices;


        public DepartmentWorkingsController(
            IDepartWorkServices departWorkServices,
            IDepartmentServices departmentServices,
            IServicesServices servicesServices,
            INationalityServices nationalityServices)
        {
            _departWorkServices = departWorkServices;
            _departmentServices = departmentServices;
            _servicesServices = servicesServices;
            _nationalityServices = nationalityServices;
        }
        public DepartmentWorkingsController(IDepartWorkServices departWorkServices)
        {
            _departWorkServices = departWorkServices;

        }
        private void createList()
        {
            //IEnumerable<Department> departments = _context.Departments.ToList();
            //SelectList selectListItems = new SelectList(departments, "Id", "Name");
            //ViewBag.Departments = selectListItems;

            //IEnumerable<Service> service = _context.Services.ToList();
            //SelectList selectListItems1 = new SelectList(service, "Id", "Name");
            //ViewBag.Service = selectListItems1;

            // IEnumerable<Department> departments = _repositoryDepartment.GetAll();
            IEnumerable<Department> departments = _departmentServices.GetAll();
            SelectList selectListItems = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = selectListItems;

            //  IEnumerable<Services> services = _repositoryService.GetAll();
            IEnumerable<Services> services = _servicesServices.GetAll();
            SelectList selectListItems1 = new SelectList(services, "Id", "Name");
            ViewBag.Services = selectListItems1;

            ViewBag.Active = new List<SelectListItem>
            {
                new SelectListItem { Text = "Yes", Value = "true" },
                new SelectListItem { Text = "No", Value = "false" },
            };
        }
        // Displays a list of all DepartmentWorking
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //IEnumerable<DepartmentWorking> departmentWorkings = _context.DepartmentWorkings
               // IEnumerable<DepartmentWorking> departmentWorkings = _repositoryDepartmentWorking.GetAll(
                IEnumerable<DepartmentWorking> departmentWorkings = _departWorkServices.GetAll();
                   //d => d.Department,
                   //s => s.Service);
                return View(departmentWorkings);
                //foreach (var item in departments)
                //{
                //    item.Uid = Guid.NewGuid().ToString();
                //    _context.departments.Update(item);
                //    _context.SaveChanges();
                //}
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }
        // create a new DepartmentWorking

        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentWorking dept)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dept);

                }
                //_unitOfWork._departWorkRepo.Add(departWork);
                //_repositoryDepartWorks.Add(departWork);
                //_context.Departments.Add(dept);
                //_context.SaveChanges();

                // _repositoryDepartmentWorking.Add(dept);
                _departWorkServices.Create(dept);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("حدث خطا غير متوقع يرجي مراجعة الدعم الفني:0565455252545");
            }
        }
        // Edit
        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            //var dept = _context.DepartmentWorkings.FirstOrDefault(e => e.Uid == Uid);
            // var dept = _repositoryDepartmentWorking.GetByUid(Uid);
            // var emps = _unitOfWork._departWorkRepo.GetByUId(Uid);

            var dept = _departWorkServices.GetByUid(Uid);
            createList();
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentWorking dept, string Uid)
        {
            try
            {
                if (dept == null)
                {
                    return NotFound();
                }
                //var appoint = _context.DepartmentWorkings
                //.AsNoTracking().FirstOrDefault(a => a.Uid == departmentWorking.Uid);
                // var depart = _repositoryDepartmentWorking.GetByUid(Uid);
                // var emplo = _unitOfWork._departWorkRepo.GetByUId(employee.Uid);

                var depart = _departWorkServices.GetByUid(Uid);
                if (depart != null)
                {
                    return NotFound();
                }

                    depart.Name = dept.Name;
                    depart.StartTime = dept.StartTime;
                    depart.EndTime = dept.EndTime;
                    depart.IsActive = dept.IsActive;
                    depart.Day = dept.Day;

                //_repositoryDepartWorks.Update(emplo);
                // _unitOfWork._departWorkRepo.Update(emplo);
                //_context.DepartmentWorkings.Update(appoint);
                //_context.SaveChanges();
                _departWorkServices.Update(Uid, depart);
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
            //  var dept = _context.DepartmentWorkings.FirstOrDefault(e => e.Uid == Uid);
            // var dept = _repositoryDepartmentWorking.GetByUid(Uid);
            // var emplo = _unitOfWork._departWorktRepo.GetByUId(Uid);

            var dept = _departWorkServices.GetByUid(Uid);
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentWorking dept)
        {
             //var department = _repositoryDepartmentWorking.GetByUid(dept.Uid);
             var department = _departWorkServices.GetByUid(dept.Uid);
            if (department == null)
            {
                return NotFound();
            }
            //var debt = _context.Departments.FirstOrDefault(d => d.Uid == department.Uid);
            //_context.DepartmentWorkings.Remove(dept);
            //_context.SaveChanges();
            _departWorkServices.DeleteByUid(dept.Uid);
           return RedirectToAction("Index");
        }

    }
}
