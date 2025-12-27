using Timely.Controllers;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;
namespace Timely.Repositories
{

    public class AccountRepo : MainRepository<Employee>, IAccountRepo
    {
        private readonly ApplicationDbContext _context;
        public AccountRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployes()
        {
            return _context.Employees.ToList();

        }
    }
}

