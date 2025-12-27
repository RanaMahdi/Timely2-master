using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class RoleService : IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Role role)
        {
            _unitOfWork._roleRepo.Add(role);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var role = _unitOfWork._roleRepo.GetByUid(uid);
            if (role == null)
            {
                return false;
            }
            _unitOfWork._roleRepo.Delete(role.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Role> GetAll()
        {
            return _unitOfWork._roleRepo.GetAll();
        }

        public Role? GetByUid(string uid)
        {
            return _unitOfWork._roleRepo.GetByUid(uid);
        }

        public bool Update(string uid, Role input)
        {
            var role = _unitOfWork._roleRepo.GetByUid(uid);
            if (role == null)
            {
                return false;
            }
            role.Name = input.Name;
            role.Permission = input.Permission;
            _unitOfWork._roleRepo.Update(role);
            _unitOfWork.Save();
            return true;
        }
    }
}
