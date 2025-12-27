using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IServicesServices
    {
        IEnumerable<Services> GetAll();
        Services? GetByUid(string uid);
        bool Create(Services services);
        bool Update(string uid, Services services);
        bool DeleteByUid(string uid);
    }
}
