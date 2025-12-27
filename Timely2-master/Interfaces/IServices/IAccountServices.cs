using Timely.Models;
namespace Timely.Interfaces.IServices
{
    public interface IAccountServices 
    {
        IEnumerable<Employee> GetAll();
        Employee? GetByUid(string Uid);
        bool Create(Employee emp);
        bool Update(string Uid, Employee emp);
        bool DeleteByUid(string Uid);
    }
}
