using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class DepartmentRepo : MainRepository<Department>, IDepartmentRepo
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }
    }
}
