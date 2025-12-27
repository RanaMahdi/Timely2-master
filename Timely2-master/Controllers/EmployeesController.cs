using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Repositories;
using Timely.Service;

namespace Timely.Controllers
{
    [Session]
    public class EmployeesController : Controller
    {

        //private readonly ApplicationDbContext _context;
        //public EmployeesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly IEmployeeRepo _employeeRepo;
        //private readonly IRepository<Employee> _repositoryEmployees;
        //private readonly IRepository<Department> _repoDepartment;
        //private readonly IRepository<DepartmentWorking> _repoDepartmentWorking;
        //private readonly IRepository<Job> _repoJob;
        //private readonly IRepository<Nationality> _repoNationality;

        //public EmployeesController( IEmployeeRepo employeeRepo,
        //    IRepository<Employee> repository,
        //    IRepository<Department> repoDepartment,
        //    IRepository<DepartmentWorking> reposDepartmentWorking,
        //    IRepository<Job> repoJob,
        //    IRepository<Nationality> repoNationalit
        ////{
        ////    _repositoryEmployees = repository;
        ////    _repositoryDepartment = repoDepartment;
        ////    _repositoryDepartmentWorking = repositoryDepartmentWorking;
        ////    _repositoryJob = repositoryJob;
        ////    _repositoryNationality = repositoryNationality;
        ////}
        //{
        //    _employeeRepo = employeeRepo;
        //    _repositoryEmployees = repository;
        //    _repoDepartment = repoDepartment;
        //    _repoDepartmentWorking = reposDepartmentWorking;
        //    _repoJob = repoJob;
        //    _repoNationality = repoNationality;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public EmployeesController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly IEmployeeServices _employeeServices;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(IEmployeeServices employeeServices, IWebHostEnvironment env)
        {
            _employeeServices = employeeServices;
            _env = env;
        }

        private void createList()
        {
            //IEnumerable<Department> departments = _unitOfWork._repositoryDepartment.GetAll();
            IEnumerable<Department> departments = _employeeServices.GetDepartments();
            SelectList selectListItems = new SelectList(departments, "Id", "Name");
            ViewBag.Departments = selectListItems;

            //IEnumerable<DepartmentWorking> departmentWorkings = _unitOfWork._repositoryDepartmentWorking.GetAll();
            IEnumerable<DepartmentWorking> departmentWorkings = _employeeServices.GetDepartmentWorkings();
            SelectList selectListItems1 = new SelectList(departmentWorkings, "Id", "Name");
            ViewBag.DepartmentWorkings = selectListItems1;

            //IEnumerable<Job> jobs = _unitOfWork._repositoryJob.GetAll();
            IEnumerable<Job> jobs = _employeeServices.GetJobs();
            SelectList selectListItems2 = new SelectList(jobs, "Id", "Name");
            ViewBag.Jobs = selectListItems2;

            //IEnumerable<Nationality> nationalities = _unitOfWork._repositoryNationality.GetAll();
            IEnumerable<Nationality> nationalities = _employeeServices.GetNationalities();
            SelectList selectListItems3 = new SelectList(nationalities, "Id", "Name");
            ViewBag.Nationalities = selectListItems3;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IEnumerable<Employee> Empls = _employeeServices.GetAll();
                return View(Empls);
            }
            catch (Exception)
            {
                return Content(" ops! ");
            }
        }

        // Action Create
        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }

        private string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("امتداد الملف غير مسموح");

            var folder = Path.Combine("uploads", "employees");
            var rootFolder = Path.Combine(_env.WebRootPath, folder);

            Directory.CreateDirectory(rootFolder);

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(rootFolder, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                file.CopyTo(stream);
            }

            var relativePath = Path.Combine(folder, fileName).Replace('\\', '/');
            return "/" + relativePath;
        }

        private void DeleteImageIfExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/')
                .Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(employee);
                }

                if (employee.ImageFile != null)
                {
                    var imagePath = SaveImage(employee.ImageFile);
                    employee.ImageUrl = imagePath;
                }

                _employeeServices.Create(employee);

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
            var emps = _employeeServices.GetByUid(Uid);
            createList();
            return View(emps);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(employee);
                }

                var emplo = _employeeServices.GetByUid(employee.Uid);
                if (emplo == null)
                {
                    return NotFound();
                }

                emplo.Name = employee.Name;
                emplo.Phone = employee.Phone;
                emplo.Password = employee.Password;
                emplo.Email = employee.Email;
                emplo.DepartmentId = employee.DepartmentId;
                emplo.JobId = employee.JobId;
                emplo.NationalityId = employee.NationalityId;

                if (employee.ImageFile != null)
                {
                    DeleteImageIfExists(emplo.ImageUrl);

                    var imagePath = SaveImage(employee.ImageFile);

                    emplo.ImageUrl = imagePath;
                }

                _employeeServices.Update(emplo.Uid, emplo);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content(" error ");
            }
        }

        // Action Delete
        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            var emplo = _employeeServices.GetByUid(Uid);
            return View(emplo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee, string Uid)
        {
            var emplo = _employeeServices.GetByUid(Uid);
            if (emplo == null)
            {
                return NotFound();
            }

            // حذف الصورة من المجلد
            DeleteImageIfExists(emplo.ImageUrl);

            _employeeServices.DeleteByUid(Uid);

            return RedirectToAction("Index");
        }
    }
}
