using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Timely.Controllers
{
    [Session]
    public class DepartmentsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        // public DepartmentsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //private readonly IRepository<Department> _repositoryDepartment;
        //public DepartmentsController( IRepository<Department> repository)
        //{
        //    _repositoryDepartment = repository;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public DepartmentController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IDepartmentServices _departmentServices;
        private readonly IWebHostEnvironment _env;
        public DepartmentsController(IDepartmentServices departmentServices, IWebHostEnvironment env)
        {
            _departmentServices = departmentServices;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //IEnumerable<Department> departments = _context.Departments.ToList();
                //  IEnumerable<Department> departments = _repositoryDepartment.GetAll();
                //foreach (var item in departments)
                //{
                //    item.Uid = Guid.NewGuid().ToString();
                //    //item.CreatedAt = DateTime.Now;
                //    _context.Departments.Update(item);
                //    _context.SaveChanges();
                //}
                IEnumerable<Department> department = _departmentServices.GetAll();

                return View(department);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        // create a new Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        private string? SaveImage(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            // التحقق من الامتداد (اختياري لكنه مهم)
            // في حاله رفع جميع انواع الملفات قد يؤدي الي مشاكل امنيه لذلك نحدد امتدادات مسموحه ونحظر الباقي مثل  .exe .dll .cs الخ
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                throw new InvalidOperationException("امتداد الملف غير مسموح");

            // مسار المجلد داخل wwwroot
            var folder = Path.Combine("uploads", "departments");
            var rootFolder = Path.Combine(_env.WebRootPath, folder);

            // إنشاء المجلد لو غير موجود
            Directory.CreateDirectory(rootFolder);

            // اسم ملف فريد
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(rootFolder, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                file.CopyTo(stream);
            }

            // نعيد المسار النسبي للاستخدام في <img src="~/{path}">
            var relativePath = Path.Combine(folder, fileName).Replace('\\', '/');
            return "/" + relativePath;
        }



        private void DeleteImageIfExists(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(department);
                }

                if (department.ImageFile != null)
                {
                    // حفظ الصورة في المجلد وإرجاع المسار النسبي
                    var imagePath = SaveImage(department.ImageFile);
                    department.ImageUrl = imagePath;
                }
                _departmentServices.Create(department);
                // _repositoryDepartment.Add(department);
                //_context.Departments.Add(department);
                //_context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        //Edit
        [HttpGet]

        public IActionResult Edit(string Uid)
        {
            //var department = _context.Departments.Find(Id);
            //var department = _repositoryDepartment.GetByUid(Uid);
            // var emps = _unitOfWork._departmentRepo.GetByUId(Uid);
            var depart = _departmentServices.GetByUid(Uid);
            return View(depart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(department);
                }
                //var dept = _context.Departments.AsNoTracking().FirstOrDefault(a => a.Uid == department.Uid);
                //var dept = _repositoryDepartment.GetByUid(Uid);
                //_context.Departments.Update(department);
                //_context.SaveChanges();
                var dept = _departmentServices.GetByUid(department.Uid);
                if (dept == null)
                {
                    return NotFound();
                }
                dept.Name = department.Name;
                dept.Description = department.Description;
                //_repositoryDepartments.Update(department);
                // _unitOfWork._departmentRepo.Update(department);

                if (department.ImageFile != null)
                {
                    // حفظ الصورة في المجلد وإرجاع المسار النسبي
                    var imagePath = SaveImage(department.ImageFile);
                    department.ImageUrl = imagePath;
                }
                _departmentServices.Update(dept.Uid, department);
                //_context.Departments.Update(department);
                //_context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني 565455252545");
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            // var department = _context.Departments.Find(Id);
            //var department = _context.Departments.FirstOrDefault(d => d.Uid.ToString() == Uid);
            // var depart = _repositoryDepartment.GetByUid(Uid);
            var depart = _departmentServices.GetByUid(Uid);
            return View(depart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department, string Uid)
        {
            // var dept = _repositoryDepartment.GetByUid(Uid);
            var depart = _departmentServices.GetByUid(Uid);
            if (depart == null)
            {
                return NotFound();
            }
            //_repositoryDepartment.Delete(dept.Id);
            //var debt = _context.Departments.FirstOrDefault(d => d.Uid == department.Uid);
            //_context.Departments.Remove(debt);
            //_context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}