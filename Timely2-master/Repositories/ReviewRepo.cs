using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class ReviewRepo : MainRepository<Review>, IReviewRepo
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetReviewsWithServiceAndClient()
        {
            return _context.Reviews
                .Include(s => s.service)
                .Include(c => c.Client)
                .ToList();
        }
    }
}
