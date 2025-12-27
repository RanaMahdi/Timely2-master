using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{

    public class EmployeeRepo : MainRepository<Employee>, IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeesWithDepartmentAndJobAndDepartmentWorkingAndNationality()
        {
            return _context.Employees
               .Include(e => e.Department)
               .Include(e => e.DepartmentWorking)
               .Include(e => e.Job)
               .Include(e => e.Nationality)
               .ToList();

        }
    }
}