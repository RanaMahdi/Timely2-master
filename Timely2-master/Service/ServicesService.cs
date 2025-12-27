using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class ServicesService : IServicesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServicesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Services services)
        {
            _unitOfWork._serviceRepo.Add(services);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var services = _unitOfWork._serviceRepo.GetByUid(uid);
            if (services == null)
            {
                return false;
            }
            _unitOfWork._serviceRepo.Delete(services.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Services> GetAll()
        {
            return _unitOfWork._serviceRepo.GetAll();
        }

        public Services? GetByUid(string uid)
        {
            return _unitOfWork._serviceRepo.GetByUid(uid);
        }

        public bool Update(string uid, Services input)
        {
            var services = _unitOfWork._serviceRepo.GetByUid(uid);
            if (services == null)
            {
                return false;
            }
            services.Name = input.Name;
            services.Price = input.Price;
            services.IsActive = input.IsActive;
            services.TypeServiceId = input.TypeServiceId;
            services.DepartmentId = input.DepartmentId;
            _unitOfWork._serviceRepo.Update(services);
            _unitOfWork.Save();
            return true;
        }
    }
}
   
