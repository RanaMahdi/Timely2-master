using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IDetailServices
    {
        IEnumerable<Detail> GetAll();
        Detail? GetByUid(string uid);
        bool Create(Detail detail);
        bool Update(string uid, Detail detail);
        bool DeleteByUid(string uid);

    }
}
