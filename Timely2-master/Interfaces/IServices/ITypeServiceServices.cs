using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface ITypeServiceServices
    {
        IEnumerable<TypeService> GetAll();
        TypeService? GetByUid(string uid);
        bool Create(TypeService typeService);
        bool Update(string uid, TypeService typeService);
        bool DeleteByUid(string uid);
    }
}
