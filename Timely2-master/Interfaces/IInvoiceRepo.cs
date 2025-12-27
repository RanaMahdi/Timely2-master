using Timely.Models;

namespace Timely.Interfaces
{
    public interface IInvoiceRepo :IRepository<Invoice>
    {
        IEnumerable<Invoice> GetInvoicesWithAppointmentAndPaymentAndClient();
    }
}
