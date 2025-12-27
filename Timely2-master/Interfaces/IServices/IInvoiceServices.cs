using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IInvoiceServices
    {
        IEnumerable<Invoice> GetAll();
        Invoice? GetByUid(string uid);
        bool Create(Invoice invoice);
        bool Update(string uid, Invoice invoice);
        bool DeleteByUid(string uid);
    }
}
