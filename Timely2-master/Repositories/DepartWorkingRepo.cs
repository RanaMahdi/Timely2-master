using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class DepartWorkingRepo : MainRepository<DepartmentWorking> ,IDepartWorkingRepo
    {
        private readonly ApplicationDbContext _context;
        public DepartWorkingRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<DepartmentWorking> GetDepartmentWorkingsWithDepartmentAndService()
        {
            return _context.DepartmentWorkings
               .Include(d => d.Department)
               .Include(s => s.Service)
               .ToList();

        }
    }
}
