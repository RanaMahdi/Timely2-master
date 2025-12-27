using Timely.Models;

namespace Timely.Interfaces
{
    public interface IServiceRepo : IRepository<Services>
    {
        IEnumerable<Services> GetServicesWithTypeServiceAndDepartment();

    }
}
