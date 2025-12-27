using Timely.Models;

namespace Timely.Interfaces
{
    public interface IPaymentRepo: IRepository<Payment>
    {
        IEnumerable<Payment> GetPaymentsWithAppointmentAndClient();
    }
}
