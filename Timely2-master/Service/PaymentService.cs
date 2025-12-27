using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class PaymentService :IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Payment payment)
        {
            _unitOfWork._paymentRepo.Add(payment);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var payment = _unitOfWork._paymentRepo.GetByUid(uid);
            if (payment == null)
            {
                return false;
            }
            _unitOfWork._paymentRepo.Delete(payment.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _unitOfWork._paymentRepo.GetAll();
        }

        public Payment? GetByUid(string uid)
        {
            return _unitOfWork._paymentRepo.GetByUid(uid);
        }

        public bool Update(string uid, Payment input)
        {
            var payment = _unitOfWork._paymentRepo.GetByUid(uid);
            if (payment == null)
            {
                return false;
            }
            payment.Amount = input.Amount;
            payment.PaymentDate = input.PaymentDate;
            payment.PaymentMethod = input.PaymentMethod;
            payment.AppointmentId = input.AppointmentId;
            payment.ClientId = input.ClientId;
            _unitOfWork._paymentRepo.Update(payment);
            _unitOfWork.Save();
            return true;
        }
    }
}
