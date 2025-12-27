using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
namespace Timely.Service
{
    public class AccountService : IAccountServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Employee emp)
        {
            _unitOfWork._employeeRepo.Add(emp);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string Uid)
        {
            var Emps = _unitOfWork._employeeRepo.GetByUid(Uid);
            if (Emps == null)
            {
                return false;
            }
            _unitOfWork._employeeRepo.Delete(Emps.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _unitOfWork._employeeRepo.GetAll();
        }

        public Employee? GetByUid(string Uid)
        {
            return _unitOfWork._employeeRepo.GetByUid(Uid);
        }

        public bool Update(string Uid, Employee emp)
        {
            var existingEmp = _unitOfWork._employeeRepo.GetByUid(Uid);
            _unitOfWork._employeeRepo.Update(existingEmp);
            _unitOfWork.Save();
            return true;
        }
    }
}
