using Timely.Models;

namespace Timely.Interfaces
{
    public interface ITypeServiceRepo : IRepository<TypeService>
    {
        IEnumerable<TypeService>GetTypeServices();
    }
}
