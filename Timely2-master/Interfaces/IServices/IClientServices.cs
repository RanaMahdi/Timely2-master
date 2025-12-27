using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IClientServices
    {
        IEnumerable<Client> GetAll();
        Client? GetByUid(string uid);
        bool Create(Client client);
        bool Update(string uid, Client client);
        bool DeleteByUid(string uid);
        void Delete(int id);
        IEnumerable<Nationality> GetNationalities();

    }
}
