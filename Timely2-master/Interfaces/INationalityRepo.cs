using Timely.Models;

namespace Timely.Interfaces
{
    public interface INationalityRepo : IRepository <Nationality>
    {
        IEnumerable<Nationality>GetNationalities();
    }
}
