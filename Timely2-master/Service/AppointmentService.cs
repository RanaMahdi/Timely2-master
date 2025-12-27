using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class AppointmentService :IAppointmentServices
    {

        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Appointment appointment)
        {
            _unitOfWork._appointmentRepo.Add(appointment);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var appointment = _unitOfWork._appointmentRepo.GetByUid(uid);
            if (appointment == null)
            {
                return false;
            }
            _unitOfWork._appointmentRepo.Delete(appointment.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _unitOfWork._appointmentRepo.GetAll();
        }

        public Appointment? GetByUid(string uid)
        {
            return _unitOfWork._appointmentRepo.GetByUid(uid);
        }

        public bool Update(string uid, Appointment appointment)
        {
            var appoint = _unitOfWork._appointmentRepo.GetByUid(uid);
            if (appoint == null)
            {
                return false;
            }
            _unitOfWork._appointmentRepo.Update(appoint);
            _unitOfWork.Save();
            return true;
        }
    }
}
   