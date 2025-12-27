using Timely.Models;

namespace Timely.Interfaces
{
    public interface IRoleRepo :IRepository<Role>
    {
        IEnumerable<Role> GetRoles();
    }
}
