using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class ServiceRepo : MainRepository<Services>, IServiceRepo
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Services> GetServicesWithTypeServiceAndDepartment()
        {
            return _context.Services
                .Include(t => t.IsActive)
                .Include(d => d.Department)
                .ToList();
        }
    }
    }

