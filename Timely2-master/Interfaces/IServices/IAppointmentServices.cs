using Timely.Models;

namespace Timely.Interfaces.IServices
{
    public interface IAppointmentServices
    {
        IEnumerable<Appointment> GetAll();
        Appointment? GetByUid(string uid);
        bool Create(Appointment appointment);
        bool Update(string uid, Appointment appointment);
        bool DeleteByUid(string uid);
      
    }
}
