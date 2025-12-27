using Timely.Models;

namespace Timely.Interfaces
{
    public interface IAppointmentRepo : IRepository<Appointment>
    {
     
        IEnumerable<Appointment> GetAppointmentsWithClientAndEmployee();

    }
}
