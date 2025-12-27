using Timely.Models;

namespace Timely.Interfaces
{
    public interface IAccountRepo
    {
        IEnumerable<Employee> GetEmployes();
    }
}
