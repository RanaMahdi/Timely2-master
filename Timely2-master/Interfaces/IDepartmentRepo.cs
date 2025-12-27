using Timely.Models;

namespace Timely.Interfaces
{
    public interface IDepartmentRepo : IRepository<Department>
    {
        IEnumerable<Department> GetDepartments();
    }
}
