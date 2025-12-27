using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely.Controllers
{
    [Session]
    public class RolesController : Controller
    {
        //private readonly Data.ApplicationDbContext _context;

        //public RolesController(Data.ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IRepository<Role> _repositoryService;
        //public RolesController(
        //    IRepository<Role> repository,
        //    _repositoryRoles = repository;
        //private readonly IUnitOfWork _unitOfWork;
        //public RolesController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;

        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _roleServices.GetAll();
           // var roles = _context.Roles.ToList();
            //foreach (var item in roles)
            //{
            //    item.Uid = Guid.NewGuid().ToString();
            //    _context.Roles.Update(item);
            //    _context.SaveChanges();
            //}
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(role);

                }
                //_context.Roles.Add(role);
                //_context.SaveChanges();
                //_repositoryRole.Add(role);
                //_unitOfWork._roleRepo.Add(role);
                _roleServices.Create(role);
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
            //  var role = _context.Roles.Find(Id);
            //  var role = _context.Roles.FirstOrDefault(e => e.Uid == Uid);
            var role = _roleServices.GetByUid(Uid);
            return View(role);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(role);
                }

                //  var role1 = _context.Roles.FirstOrDefault(e => e.Uid == role.Uid);
                var roles = _roleServices.GetByUid(Uid);
                if (roles == null)
                {
                    return NotFound();
                }
                roles.Name = role.Name;
                roles.Permission = role.Permission;
                _roleServices.Update(Uid, roles);

                //_context.Roles.Update(role1);
                //_context.SaveChanges();
                //var role = _repositoryRole.GetByUid(Uid);
                //_repositoryRole.Update(role);
                //_unitOfWork._roleRepo.Add(role);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("لحل المشكلة لطفاً تواصل مع الدعم الفني565455252545");
            }
        }

        [HttpGet]
        public IActionResult Delete(string Uid)
        {
            // var role = _context.Roles.AsNoTracking().FirstOrDefault(e => e.Uid == Uid);
            //var role = _context.Roles.Find(Id);
            // var role = _repositoryRole.Find(Id);
            // var role = _unitOfWork._roleRepo.Find(Id);
            var rev = _roleServices.GetByUid(Uid);
            return View(rev);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Role role, string Uid)
        {
            // var debt = _context.Roles.FirstOrDefault(d => d.Uid == role.Uid);
            var rol = _roleServices.GetByUid(Uid);
            if (rol == null)
            {
                return NotFound();
            }

            //_context.Roles.Remove(role);
            //_context.SaveChanges();
            //var role = _context.Roles.Find(Id);
            // var role = _repositoryRole.Find(Id);
            // var role = _unitOfWork._roleRepo.Find(Id);
            _roleServices.DeleteByUid(Uid);
            return RedirectToAction("Index");
        }

    }
}