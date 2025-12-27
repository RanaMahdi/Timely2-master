using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely.Controllers
{
    [Session]
    public class UsersController : Controller
    {
        //private readonly ApplicationDbContext _context
        //public UsersController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IRepository<User> _repositoryUsers;
        //private readonly IRepository<Role> _repositoryRoles;
        //public UsersController(IRepository<User> repository, 
        //    IRepository<Role> repositoryRoles)
        //{
        //    _repositoryUsers = repository;
        //    _repositoryRoles = repositoryRoles;
        //}
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;
        public UsersController(IUserServices userServices, IRoleServices roleServices)

        {

            this.userServices = userServices;
            this.roleServices = roleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<User> use = _context.Users
            //   IEnumerable<User> use = _repositoryUsers.GetAll(
            // r => r.Roles);

            //.Include(c => c.Nationality)
            //.Include(r=> r.Roles)
            //.ToList();
            IEnumerable<User> use = userServices.GetAll();
            return View(use);
        }

        private void createList()
        {
            // IEnumerable<Role> rols = _repositoryRoles.GetAll();
            IEnumerable<Role> roles = roleServices.GetAll();

            ViewBag.Roles = roles;

            ViewBag.Genders = new List<SelectListItem>
            {
                new SelectListItem { Text="Male", Value="M" },
                new SelectListItem { Text="Female", Value="F" },
            };
        }

        //// Action Create
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    createList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(User users)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            createList();
        //            return View(users);
        //        }
        //        // _repositoryUsers.Add(Users);
        //        //_context.Users.Add(Users);
        //        //_context.SaveChanges();
        //        userServices.Create(users);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return Content(" Error ");
        //    }

        //}

        //// Action Edit
        //[HttpGet]
        //public IActionResult Edit(int Id)
        //{
        //    //var use = _context.Users.Find(Id);
        //    // var use = _repositoryUsers.GetById(Id);
        //    var use = userServices.GetByUid(Id);

        //    createList();
        //    return View(use);
        //}

        //[HttpPost]
        //public IActionResult Edit(User user)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            createList();
        //            return View(user);
        //        }
        //        // _repositoryUsers.Update(user);
        //        //_context.Users.Update(user);
        //        //_context.SaveChanges();
        //        userServices.Update(user);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return Content(" Error ");
        //    }
        //}

        //// Action Delete
        //[HttpGet]
        //public IActionResult Delete(int Id)
        //{
        //    // var use = _repositoryUsers.GetById(Id);
        //    //var use = _context.Users.Find(Id);
        //    var use = userServices.GetByUid(Id);
        //    createList();
        //    return View(use);
        //}

        //[HttpPost]
        //public IActionResult Delete(User user)
        //{
        //    try
        //    {
        //        // _repositoryUsers.Delete(user.Id);
        //        //_context.Users.Remove(user);
        //        //_context.SaveChanges();
        //        userServices.DeleteByUid(user.Id);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return Content(" Error ");
        //    }
        //}

    }
}