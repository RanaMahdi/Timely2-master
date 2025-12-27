using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timely.Dtos;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Service;

namespace Timely2_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;
        public ReviewsController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            IEnumerable<Review> reviews = _reviewServices.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{uid}")]
        public ActionResult<Review> GetByUid(string uid)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uid))
                    return BadRequest("Uid is required.");

                var review = _reviewServices.GetByUid(uid);
                if (review == null)
                    return NotFound("لا توجد فئه بهذا الرقم"); // 404

                return Ok(review); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }

        [HttpPost]
        public ActionResult Create(ReviewDto reviewDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var review = new Review();

                review.Comments = reviewDto.Comments;
                review.Rating = reviewDto.Rating;
                review.ReviewDate = reviewDto.ReviewDate;
                review.ServiceId = reviewDto.ServiceId;
                review.ClientId = reviewDto.ClientId;


                var isCreated = _reviewServices.Create(review);
                if (isCreated)
                    return Ok("تم انشاء الفئه بنجاح"); // 200

                return BadRequest("فشل انشاء الفئه"); // 400

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }
        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] ReviewUpdateDto review)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            if (review == null)
                return BadRequest("Category payload is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var exists = _reviewServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            // نضمن التحديث على نفس الـUid

            var newReview = new Review
            {

                Uid = review.Uid,
                Comments = review.Comments,
                Rating = review.Rating,
                ReviewDate = review.ReviewDate,
                ServiceId = review.ServiceId,
                ClientId = review.ClientId,
            };






            _reviewServices.Update(uid, newReview);
            return Ok("تم تحديث الفئة بنجاح");
        }


        // DELETE: api/reviews/{uid}
        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid))
                return BadRequest("Uid is required.");

            var exists = _reviewServices.GetByUid(uid);
            if (exists == null)
                return NotFound(); // 404

            _reviewServices.DeleteByUid(uid);
            return Ok("تم حذف الفئة بنجاح"); // 204
        }
    }
}