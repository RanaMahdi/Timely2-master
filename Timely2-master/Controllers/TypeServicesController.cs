 using Microsoft.AspNetCore.Mvc;
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
    public class TypeServicesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //private readonly IRepository<TypeService> _repositoryTypeService;
        //public TypeServicesController(IRepository<TypeService> repository)
        //{
        //    _repositoryTypeService = repository;
        //}
        //public TypeServicesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        private readonly ITypeServiceServices _typeServiceServices;
        public TypeServicesController( ITypeServiceServices typeServiceServices
            )
        { 
            _typeServiceServices = typeServiceServices;
            }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // IEnumerable<TypeServic> typeServices = _context.TypeService.ToList();
                // IEnumerable<TypeService> typeServices = _repositoryTypeService.GetAll();
                IEnumerable<TypeService> typeService = _typeServiceServices.GetAll();

                //foreach (var item in typeServices)
                //{
                //    item.Uid = Guid.NewGuid().ToString();
                //    //item.CreatedAt = DateTime.Now;
                //    _context.TypeServices.Update(item);
                //    _context.SaveChanges();
                //}
                return View(typeService);
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TypeService typeService)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(typeService);
                }
                //_context.TypeServices.Remove(typeService);
                //_context.SaveChanges();
                // _repositoryTypeService.Add(typeService);
                _typeServiceServices.Create(typeService);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("حدث أثناء الحفظ ");
            }
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(string Uid)
        {
            // var typeService = _context.TypeServices.FirstOrDefault(e => e.Uid == Uid);
           // var typeService = _repositoryTypeService.GetByUid(Uid);
            var typeService = _typeServiceServices.GetByUid(Uid);

            return View(typeService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TypeService typeService, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(typeService);
                }
                // var cate = _context.TypeServices.FirstOrDefault(e => e.Uid == Uid);
               // var type = _repositoryTypeService.GetByUid(Uid);
                var type = _typeServiceServices.GetByUid(Uid);
                if (type == null)
                {
                    return NotFound();
                }
                type.Name = typeService.Name;
                    type.Description = typeService.Description;
                    //_context.TypeServices.Remove(typeService);
                    //_context.SaveChanges();
                   // _repositoryTypeService.Update(type);
                    _typeServiceServices.DeleteByUid(Uid);
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
            // var typeService = _context.TypeService.Find(Id);
           // var typeService = _repositoryTypeService.GetByUid(Uid);
            var typeService = _typeServiceServices.GetByUid(Uid);
            return View(typeService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TypeService typeService ,string Uid)
        {
               // var typeServices = _repositoryTypeService.GetByUid(typeService.Uid);
                var typeServices = _typeServiceServices.GetByUid(typeService.Uid);
                if (typeServices == null)
                {
                    return NotFound();
                }
            //_context.TypeServices.Remove(typeService);
            //_context.SaveChanges();
            // _repositoryTypeService.Delete(typeService.Id);
            _typeServiceServices.DeleteByUid(Uid);
                return RedirectToAction("Index");
        }
    }
}
        