using Timely.Models;

namespace Timely.Interfaces
{
    public interface IClientRepo : IRepository<Client>
    {
        IEnumerable<Client> GetClientswithNationality();
    }
}
