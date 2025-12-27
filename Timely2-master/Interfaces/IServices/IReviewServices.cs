using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IReviewServices
    {
        IEnumerable<Review> GetAll();
        Review? GetByUid(string uid);
        bool Create(Review review);
        bool Update(string uid, Review review);
        bool DeleteByUid(string uid);
    }
}
