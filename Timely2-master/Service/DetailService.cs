using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class DetailService :IDetailServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Detail details)
        {
            _unitOfWork._detailsRepo.Add(details);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var details = _unitOfWork._detailsRepo.GetByUid(uid);
            if (details == null)
            {
                return false;
            }
            _unitOfWork._detailsRepo.Delete(details.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Detail> GetAll()
        {
            return _unitOfWork._detailsRepo.GetAll();
        }

        public Detail? GetByUid(string uid)
        {
            return _unitOfWork._detailsRepo.GetByUid(uid);
        }

        public bool Update(string uid, Detail input)
        {
            var details = _unitOfWork._detailsRepo.GetByUid(uid);
            if (details == null)
            {
                return false;
            }
            details.Name = input.Name;
            details.Description = input.Description;
            details.CreatedDate = input.CreatedDate;
            details.AppointmentId = input.AppointmentId;
            _unitOfWork._detailsRepo.Update(details);
            _unitOfWork.Save();
            return true;
        }
    }
}
