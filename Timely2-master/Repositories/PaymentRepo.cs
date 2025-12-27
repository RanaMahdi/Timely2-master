using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class PaymentRepo : MainRepository<Payment>, IPaymentRepo
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetPaymentsWithAppointmentAndClient()
        {
            return _context.Payments
                .Include(a => a.Appointment)
                .Include(c => c.Client)
                .ToList();
        }
    }
}
