using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class JobRepo : MainRepository<Job>, IJobRepo
    {
        private readonly ApplicationDbContext _context;
        public JobRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Job> GetJobs()
        {
           return _context.Jobs;
        }
    }
}
