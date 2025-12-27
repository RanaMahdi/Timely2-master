using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class RoleRepo : MainRepository<Role>, IRoleRepo
    {
        private readonly ApplicationDbContext _context;
        public RoleRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetRoles()
        {
           return _context.Roles;
        }
    }
}
