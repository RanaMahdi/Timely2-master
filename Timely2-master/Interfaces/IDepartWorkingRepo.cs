using Microsoft.EntityFrameworkCore;
using Timely.Models;

namespace Timely.Interfaces
{
    public interface IDepartWorkingRepo : IRepository<DepartmentWorking>
    {
        public IEnumerable<DepartmentWorking> GetDepartmentWorkingsWithDepartmentAndService();
    }
}