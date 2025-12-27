using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class TypeServiceService : ITypeServiceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(TypeService typeService)
        {
            _unitOfWork._typeServiceRepo.Add(typeService);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var typeService = _unitOfWork._typeServiceRepo.GetByUid(uid);
            if (typeService == null)
            {
                return false;
            }
            _unitOfWork._typeServiceRepo.Delete(typeService.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<TypeService> GetAll()
        {
            return _unitOfWork._typeServiceRepo.GetAll();
        }

        public TypeService? GetByUid(string uid)
        {
            return _unitOfWork._typeServiceRepo.GetByUid(uid);
        }

        public bool Update(string uid, TypeService input)
        {
            var typeService = _unitOfWork._typeServiceRepo.GetByUid(uid);
            if (typeService == null)
            {
                return false;
            }
            typeService.Name = input.Name;
            typeService.Description = input.Description;
            _unitOfWork._typeServiceRepo.Update(typeService);
            _unitOfWork.Save();
            return true;
        }
    }
}
