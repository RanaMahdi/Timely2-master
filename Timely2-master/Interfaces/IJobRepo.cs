using Timely.Models;

namespace Timely.Interfaces
{
    public interface IJobRepo :IRepository<Job>
    {
        IEnumerable<Job> GetJobs();
    }
}
