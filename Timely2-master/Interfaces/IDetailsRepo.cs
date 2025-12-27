using Timely.Models;

namespace Timely.Interfaces
{
    public interface IDetailsRepo :IRepository<Detail>
    {
        IEnumerable<Detail> GetDetailsWithAppointment();
    }
}
