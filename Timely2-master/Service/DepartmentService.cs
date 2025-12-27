using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class DepartmentService :IDepartmentServices
    {

        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Department department)
        {
            _unitOfWork._departmentRepo.Add(department);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var department = _unitOfWork._departmentRepo.GetByUid(uid);
            if (department == null)
            {
                return false;
            }
            _unitOfWork._departmentRepo.Delete(department.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Department> GetAll()
        {
            return _unitOfWork._departmentRepo.GetAll();
        }

        public Department? GetByUid(string uid)
        {
            return _unitOfWork._departmentRepo.GetByUid(uid);
        }

        public bool Update(string uid, Department input)
        {
            var department = _unitOfWork._departmentRepo.GetByUid(uid);
            if (department == null)
            {
                return false;
            }
            department.Name = input.Name;
            department.Description = input.Description;
            _unitOfWork._departmentRepo.Update(department);
            _unitOfWork.Save();
            return true;
        }
    }
}
