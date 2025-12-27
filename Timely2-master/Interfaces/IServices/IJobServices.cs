using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IJobServices
    {
        IEnumerable<Job> GetAll();
        Job? GetByUid(string uid);
        bool Create(Job job);
        bool Update(string uid, Job job);
        bool DeleteByUid(string uid);
    }
}
