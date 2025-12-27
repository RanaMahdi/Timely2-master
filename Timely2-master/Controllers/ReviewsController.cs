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
    public class ReviewsController : Controller
    {
        //private readonly Data.ApplicationDbContext _context;

        //public ReviewsController(Data.ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly IRepository<Services> _repositoryService;
        //private readonly IRepository<Client> _repositoryClient;

        //public PaymentsController(
        //    IRepository<Services> repository,
        //    IRepository<Client> repositoryClient,
        //{
        //    _repositoryServices = repository;
        //    _repositoryClient = repositoryClient;
        //}
        //private readonly IUnitOfWork _unitOfWork;
        //public ReviewsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        private readonly IReviewServices _reviewServices;
        private readonly IClientServices _clientServices;
        private readonly IServicesServices _servicesServices;
        public ReviewsController(
          IReviewServices reviewServices,
          IClientServices clientService,
         IServicesServices servicesServices)
        {
            _reviewServices = reviewServices;
            _clientServices = clientService;
            _servicesServices = servicesServices;
        }

        private void createList()
        {
            //IEnumerable<Client> clients = _context.Clients.ToList();
            //SelectList selectListItems = new SelectList(clients, "Id", "Name");
            //ViewBag.Clients = selectListItems;


            //IEnumerable<Services> services = _context.Services.ToList();
            //SelectList selectListItems1 = new SelectList(services, "Id", "Name");
            //ViewBag.Services = selectListItems1;

            IEnumerable<Client> clients = _clientServices.GetAll();
            SelectList selectListItems = new SelectList(clients, "Id", "Name");
            ViewBag.Clients = selectListItems;


            IEnumerable<Services> services = _servicesServices.GetAll();
            SelectList selectListItems1 = new SelectList(services, "Id", "Name");
            ViewBag.Services = selectListItems1;
        }
        [HttpGet]
        public IActionResult Index()
        {

            IEnumerable<Review> reviews = _reviewServices.GetAll();
            //IEnumerable<Review> reviews = _repositoryReview.GetAll(
            //IEnumerable<Review> reviews = _context.Reviews
            //    .Include(c => c.Client)
            //    .Include(c => c.service)
            //    .ToList();
            return View(reviews);
        }
        [HttpGet]
        public IActionResult Create()
        {
            createList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Review review)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(review);

                }
                //_context.Reviews.Add(review);
                //_context.SaveChanges();
                //_repositoryReview.Add(review);
                //_unitOfWork._reviewRepo.Add(review);
                _reviewServices.Create(review);
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
            //  var review = _context.Reviews.Find(Id);
            //var review = _repositoryReview.GetByUid(Uid);

            var review = _reviewServices.GetByUid(Uid);
            createList();
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Review review, string Uid)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(review);

                }
                //_context.Reviews.Update(review);
                //_context.SaveChanges();
                //var review = _context.Reviews.AsNoTracking().FirstOrDefault(a => a.Uid == review.Uid);
                //var review = _repositoryReview.GetByUid(Uid);
                //_repositoryReview.Update(review);
                //_unitOfWork._reviewRepo.Add(review);
                var rev = _reviewServices.GetByUid(Uid);
                if (rev == null)
                {
                    return NotFound();
                }
                rev.Comments = review.Comments;
                rev.Rating = review.Rating;
                rev.ReviewDate = review.ReviewDate;
                rev.ServiceId = review.ServiceId;
                rev.ClientId = review.ClientId;
                _reviewServices.Update(Uid, rev);
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
            //var review = _context.Reviews.Find(Id);
            // var review = _repositoryReview.Find(Id);
            // var review = _unitOfWork._reviewtRepo.Find(Id);
            var rev = _reviewServices.GetByUid(Uid); 
            return View(rev); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Review review, string Uid)
        {
            var rev = _reviewServices.GetByUid(Uid);
            if (rev == null)
            {
                return NotFound();
            }
            _reviewServices.DeleteByUid(Uid);
            return RedirectToAction("Index");
        }
        //var review = _context.Reviews.Find(Id);
        // var review = _repositoryReview.Find(Id);
        // var review = _unitOfWork._reviewtRepo.Find(Id);
    }
}
