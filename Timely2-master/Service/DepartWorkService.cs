using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class DepartWorkService : IDepartWorkServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartWorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(DepartmentWorking departmentWorking)
        {
            _unitOfWork._departWorkingRepo.Add(departmentWorking);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var deptWork = _unitOfWork._departWorkingRepo.GetByUid(uid);
            if (deptWork == null)
            {
                return false;
            }
            _unitOfWork._departWorkingRepo.Delete(deptWork.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<DepartmentWorking> GetAll()
        {
            return _unitOfWork._departWorkingRepo.GetAll();
        }

        public DepartmentWorking? GetByUid(string uid)
        {
            return _unitOfWork._departWorkingRepo.GetByUid(uid);
        }

        public bool Update(string uid, DepartmentWorking depWork)
        {
            var deptWork = _unitOfWork._departWorkingRepo.GetByUid(uid);
            if (deptWork == null)
            {
                return false;
            }
            deptWork.Name = depWork.Name;
            deptWork.StartTime = depWork.StartTime;
            deptWork.EndTime = depWork.EndTime;
            deptWork.IsActive = depWork.IsActive;
            deptWork.Day = depWork.Day;
            deptWork.DepartmentId = depWork.DepartmentId;
            deptWork.ServiceId = depWork.ServiceId;
            _unitOfWork._departWorkingRepo.Update(deptWork);
            _unitOfWork.Save();
            return true;
        }
    }
}