using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IDepartWorkServices
    {
        IEnumerable<DepartmentWorking> GetAll();
        DepartmentWorking? GetByUid(string uid);
        bool Create(DepartmentWorking departmentWorking);
        bool Update(string uid, DepartmentWorking departmentWorking);
        bool DeleteByUid(string uid);
       
    }
}
