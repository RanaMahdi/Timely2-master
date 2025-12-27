using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;
using Timely.Repositories;
using Timely.Service;

namespace Timely.Service
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(Employee employee)
        {
            _unitOfWork._employeeRepo.Add(employee);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var employee = _unitOfWork._employeeRepo.GetByUid(uid);
            if (employee == null)
            {
                return false;
            }
            _unitOfWork._employeeRepo.Delete(employee.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _unitOfWork._employeeRepo.GetAll();
        }

        public Employee? GetByUid(string uid)
        {
            return _unitOfWork._employeeRepo.GetByUid(uid);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _unitOfWork._departmentRepo.GetAll();
        }

        public IEnumerable<DepartmentWorking> GetDepartmentWorkings()   
        {
            return _unitOfWork._departWorkingRepo.GetAll();
        }

        public IEnumerable<Job> GetJobs()
        {
           return _unitOfWork._jobRepo.GetAll();
        }

        public IEnumerable<Nationality> GetNationalities()
        {
         return _unitOfWork._nationalityRepo.GetAll();
        }

        public bool Update(string uid, Employee input)
        {
            var employee = _unitOfWork._employeeRepo.GetByUid(uid);
            if (employee == null)
            {
                return false;
            }
            employee.Name = input.Name;
            employee.Phone = input.Phone;
            employee.Password = input.Password;
            employee.Email = input.Email;
            employee.DepartmentId = input.DepartmentId;
            employee.DepartmentWorkingId = input.DepartmentWorkingId;
            employee.JobId = input.JobId;
            employee.NationalityId = input.NationalityId;
            _unitOfWork._employeeRepo.Update(employee);
            _unitOfWork.Save();
            return true;
        }
    }
}

