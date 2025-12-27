using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IEmployeeServices
    {

        IEnumerable<Employee> GetAll();
        Employee? GetByUid(string uid);
        bool Create(Employee category);
        bool Update(string uid, Employee input);
        bool DeleteByUid(string uid);
        IEnumerable<Department> GetDepartments();
        IEnumerable<DepartmentWorking> GetDepartmentWorkings();
        IEnumerable<Job> GetJobs();
        IEnumerable<Nationality> GetNationalities();

    }
}
