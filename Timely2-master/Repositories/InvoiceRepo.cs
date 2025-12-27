using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class InvoiceRepo : MainRepository<Invoice>, IInvoiceRepo
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Invoice> GetInvoicesWithAppointmentAndPaymentAndClient()
        {
            return _context.Invoices
                .Include(a =>a.Appointment)
                .Include(p =>p.Payment)
                .Include(p => p.Client)
                .ToList();

               
        }
    }
}
