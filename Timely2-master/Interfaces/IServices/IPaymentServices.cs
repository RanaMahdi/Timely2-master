using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IPaymentServices
    {
        IEnumerable<Payment> GetAll();
        Payment? GetByUid(string uid);
        bool Create(Payment payment);
        bool Update(string uid, Payment payment);
        bool DeleteByUid(string uid);
    }
}
