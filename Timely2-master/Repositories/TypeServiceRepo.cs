using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class TypeServiceRepo : MainRepository<TypeService>, ITypeServiceRepo
    {
        private readonly ApplicationDbContext _context;
        public TypeServiceRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<TypeService> GetTypeServices()
        {
           return _context.TypeServices;
        }
    }
}
