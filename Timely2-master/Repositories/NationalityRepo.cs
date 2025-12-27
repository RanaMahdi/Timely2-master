using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class NationalityRepo : MainRepository<Nationality>, INationalityRepo
    {
        private readonly ApplicationDbContext _context;
        public NationalityRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Nationality> GetNationalities()
        {
          return _context.Nationalities;
        }
    }
}
