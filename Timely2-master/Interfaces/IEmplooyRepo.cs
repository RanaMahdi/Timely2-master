using Timely.Models;

namespace Timely.Interfaces
{
    public interface IEmployeeRepo : IRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesWithDepartmentAndJobAndDepartmentWorkingAndNationality();
    }
}