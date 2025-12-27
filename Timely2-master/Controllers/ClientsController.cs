using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Timely.Controllers
{
    [Session]

    public class ClientsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //private readonly IRepository<Client> _repositoryClient;
        //private readonly IRepository<Nationality> _repositoryNationality;

        //public ClientsController(
        //    IRepository<Client> repositoryClient,
        //    IRepository<Nationality> repositoryNationality)
        //{
        //    _repositoryClient = repositoryClient;
        //    _repositoryNationality = repositoryNationality;
        //}
        //public ClientsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public ClientsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IClientServices _clientServices;
        public ClientsController(IClientServices clientServices)
        {
            _clientServices = clientServices;

        }
        private void CreateList()
        {
            //IEnumerable<Nationality> nationalities = _context.nationalities.ToList();
            //SelectList selectListItems = new SelectList(nationalities, "Id", "Name");
            //ViewBag.nationalities = selectListItems;

            // IEnumerable<Nationality> nationalities = _repositoryNationality.GetAll();

            IEnumerable<Nationality> nationalities = _clientServices.GetNationalities();
            SelectList selectListItems = new SelectList(nationalities, "Id", "Name");
            ViewBag.Nationalities = selectListItems;
        
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var clients = _context.Clients.Include(c => c.Nationality).ToList();
            // IEnumerable<Client> clients = _repositoryClient.GetAll(
            IEnumerable<Client> clients = _clientServices.GetAll();
               // n => n.Nationality);

            //foreach (var item in clients)
            //{
            //    item.Uid = Guid.NewGuid().ToString();
            //    _context.Clients.Update(item);
            //    _context.SaveChanges();
            //}
            return View(clients);

        }
        // Create a new Client
        [HttpGet]
        public IActionResult Create()
        {
            CreateList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(client);
                }
                //_context.Clients.Add(client);
                //_context.SaveChanges();
               // _repositoryClient.Add(client);
                _clientServices.Create(client);
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
            //var client = _context.Clients.FirstOrDefault(e => e.Uid == Uid);
           // var client = _repositoryClient.GetByUid(Uid);
            var client = _clientServices.GetByUid(Uid);
            CreateList();
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(client);
                }
                //var clien = _context.Clients.AsNoTracking().FirstOrDefault(a => a.Uid == Client.Uid);
               // var clien = _repositoryClient.GetByUid(Uid);
                var clien = _clientServices.GetByUid(Uid);
                if (clien == null)
                {
                    return NotFound();
                }
                clien.Name = client.Name;
                clien.Phone = client.Phone;
                clien.Email = client.Email;
                clien.Password = client.Password;
                clien.Gender = client.Gender;
                clien.NationalityId = client.NationalityId;
                //appointment.UpdatedAt = DateTime.Now;
                //_repositoryClient.Update(clien);
                //_context.Appointments.Update(appoint);
                //_context.SaveChanges();
                _clientServices.Update(Uid, clien); 

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
            //var client = _context.Clients.AsNoTracking().FirstOrDefault(a => a.Uid == Uid);
           // var client = _repositoryClient.GetByUid(Uid);
            var client = _clientServices.GetByUid(Uid);
            CreateList();
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Client client, string Uid)
        {
            //var clien = _repositoryClient.GetByUid(Uid);
            var clien = _clientServices.GetByUid(Uid);
            if (clien == null)
            {
                return NotFound();
            }
            _clientServices.Delete(clien.Id);
            // _repositoryClient.Delete(clien.Id);
            //var debt = _context.Departments.FirstOrDefault(d => d.Uid == department.Uid);
            //_context.Departments.Remove(debt);
            //_context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}