using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class ReviewService :IReviewServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Review review)
        {
            _unitOfWork._reviewRepo.Add(review);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var review = _unitOfWork._reviewRepo.GetByUid(uid);
            if (review == null)
            {
                return false;
            }
            _unitOfWork._reviewRepo.Delete(review.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Review> GetAll()
        {
            return _unitOfWork._reviewRepo.GetAll();
        }

        public Review? GetByUid(string uid)
        {
            return _unitOfWork._reviewRepo.GetByUid(uid);
        }

        public bool Update(string uid, Review input)
        {
            var review = _unitOfWork._reviewRepo.GetByUid(uid);
            if (review == null)
            {
                return false;
            }
            review.Comments = input.Comments;
            review.Rating = input.Rating;
            review.ReviewDate = input.ReviewDate;
            review.ServiceId = input.ServiceId;
            review.ClientId = input.ClientId;
            _unitOfWork._reviewRepo.Update(review);
            _unitOfWork.Save();
            return true;
        }
    }
}

