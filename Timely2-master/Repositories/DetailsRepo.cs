using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class DetailsRepo : MainRepository<Detail>, IDetailsRepo
    {
        private readonly ApplicationDbContext _context;
        public DetailsRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Detail> GetDetailsWithAppointment()
        {
            return _context.Details.Include(a => a.Appointment);
        }
    }
}
