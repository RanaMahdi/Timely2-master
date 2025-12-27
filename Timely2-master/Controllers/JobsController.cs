using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timely.Data;
using Timely.Filters;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Timely.Controllers
{
    [Session]
    public class JobsController : Controller
    {

        //private readonly ApplicationDbContext _context;
        //public JobsController(ApplicationDbContext context)
        //{
        //    _context = context;

        ////}
        //private readonly IRepository<Job> _repositoryJob;
        //public JobsController(IRepository<Job> repository)
        //{
        //    _repositoryJob = repository;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public JobssController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IJobServices _jobServices;
        public JobsController(IJobServices jobServices)
        {
            _jobServices = jobServices;

        }
        [HttpGet]
        public IActionResult Index()
        {
            //IEnumerable<Job> jobs = _context.Jobs.ToList();
            //foreach (var item in jobs)
            //{
            //    item.Uid = Guid.NewGuid().ToString();
            //    _context.Jobs.Update(item);
            //    _context.SaveChanges();
            //}
           // IEnumerable<Job> jobs = _repositoryJob.GetAll();
            IEnumerable<Job> jobs = _jobServices.GetAll();
            return View(jobs);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Job job)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(job);
                }
                //_context.Jobs.Add(job);
                //_context.SaveChanges();
                // _repositoryJob.Add(job);
                // _jobRepo.Add(job);
                _jobServices.Create(job);

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
            //var job = _context.Jobs.FirstOrDefault(d => d.Uid.ToString() == Uid);
            // var job = _repositoryJob.GetByUid(Uid);
            //var job = _repositoryJob.GetByUId(Uid);

            var job = _jobServices.GetByUid(Uid);
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Job job, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(job);
                }
                // var job = _context.Jobs.FirstOrDefault(e => e.Uid == Uid);
                //var job = _jobRepo.GetByUId(Uid);
                // var job = _repositoryJob.GetByUid(Uid);
                var jobs = _jobServices.GetByUid(Uid);
                if (jobs == null)
                {
                    return NotFound();
                }

                jobs.Name = job.Name;
                jobs.BaseSalary = job.BaseSalary;
                //_context.Jobs.Update(job);
                //_context.SaveChanges();
                //_jobRepo.Update(appoint);
                // _repositoryJob.Update(jb);
                _jobServices.Update(Uid, jobs);
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

            //var job = _context.Jobs.FirstOrDefault(d => d.Uid == Uid);
            // var job = _jobsRepo.GetByUId(Uid);
            // var job = _repositoryJob.GetByUid(Uid);
            var job = _jobServices.GetByUid(Uid);
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Job job,string Uid)
        {
            var joob = _jobServices.GetByUid(Uid);
            if (joob == null)
            {
                return NotFound();
            }
            _jobServices.DeleteByUid(Uid);

          //  var joob = _repositoryJob.GetByUid(Uid);
            // _repositoryJob.Delete(joob.Id);
            //var joob = _context.Jobs.FirstOrDefault(d => d.Uid == job.Uid);
            //_context.Jobs.Remove(joob);
            //_context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}