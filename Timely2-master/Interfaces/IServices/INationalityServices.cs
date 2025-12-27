using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface INationalityServices
    {
        IEnumerable<Nationality> GetAll();
        Nationality? GetByUid(string uid);
        bool Create(Nationality nationality);
        bool Update(string uid, Nationality nationality);
        bool DeleteByUid(string uid);
    }
}
