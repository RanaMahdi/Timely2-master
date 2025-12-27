using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class InvoiceService :IInvoiceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Invoice invoice)
        {
            _unitOfWork._invoiceRepo.Add(invoice);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var invoice = _unitOfWork._invoiceRepo.GetByUid(Uid);
            if (invoice == null)
            {
                return false;
            }
            _unitOfWork._invoiceRepo.Delete(invoice.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _unitOfWork._invoiceRepo.GetAll();
        }

        public Invoice? GetByUid(string Uid)
        {
            return _unitOfWork._invoiceRepo.GetByUid(Uid);
        }

        public bool Update(string Uid, Invoice input)
        {
            var invoice = _unitOfWork._invoiceRepo.GetByUid(Uid);
            if (invoice == null)
            {
                return false;
            }
            invoice.InvoiceDate = input.InvoiceDate;
            invoice.TotalAmount = input.TotalAmount;
            invoice.IsPaid = input.IsPaid;
            invoice.AppointmentId = input.AppointmentId;
            invoice.PaymentId = input.PaymentId;
            invoice.ClientId = input.ClientId;
            _unitOfWork._invoiceRepo.Update(invoice);
            _unitOfWork.Save();
            return true;
        }
    }
}
   