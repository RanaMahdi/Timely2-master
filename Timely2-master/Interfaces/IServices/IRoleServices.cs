using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IRoleServices
    {
        IEnumerable<Role> GetAll();
        Role? GetByUid(string uid);
        bool Create(Role role);
        bool Update(string uid, Role role);
        bool DeleteByUid(string uid);
    }
}
