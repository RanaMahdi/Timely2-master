using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class NationalityService :INationalityServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public NationalityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Nationality nationality)
        {
            _unitOfWork._nationalityRepo.Add(nationality);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var nationality = _unitOfWork._nationalityRepo.GetByUid(uid);
            if (nationality == null)
            {
                return false;
            }
            _unitOfWork._nationalityRepo.Delete(nationality.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Nationality> GetAll()
        {
            return _unitOfWork._nationalityRepo.GetAll();
        }

        public Nationality? GetByUid(string uid)
        {
            return _unitOfWork._nationalityRepo.GetByUid(uid);
        }

        public bool Update(string uid, Nationality input)
        {
            var nationality = _unitOfWork._nationalityRepo.GetByUid(uid);
            if (nationality == null)
            {
                return false;
            }
            nationality.Name = input.Name;
            nationality.Description = input.Description;
            _unitOfWork._nationalityRepo.Update(nationality);
            _unitOfWork.Save();
            return true;
        }
    }
}
