using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class AppointmentRepo : MainRepository<Appointment>, IAppointmentRepo
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAppointmentsWithClientAndEmployee()
        {
            return _context.Appointments.Include(e => e.Employee);
        }
    }
}
