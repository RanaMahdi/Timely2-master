using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IDepartmentServices
    {
        IEnumerable<Department> GetAll();
        Department? GetByUid(string uid);
        bool Create(Department department);
        bool Update(string uid, Department department);
        bool DeleteByUid(string uid);
    }
}
