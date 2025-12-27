using Timely.Models;

namespace Timely.Interfaces
{
    public interface IReviewRepo : IRepository<Review>
    {
        IEnumerable<Review> GetReviewsWithServiceAndClient();
    }
}
