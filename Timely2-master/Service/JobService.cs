using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely.Service
{
    public class JobService :IJobServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Job job)
        {
            _unitOfWork._jobRepo.Add(job);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteByUid(string uid)
        {
            var job = _unitOfWork._jobRepo.GetByUid(uid);
            if (job == null)
            {
                return false;
            }
            _unitOfWork._jobRepo.Delete(job.Id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<Job> GetAll()
        {
            return _unitOfWork._jobRepo.GetAll();

        }

        public Job? GetByUid(string uid)
        {
            return _unitOfWork._jobRepo.GetByUid(uid);
        }

        public bool Update(string uid, Job input)
        {
            var job = _unitOfWork._jobRepo.GetByUid(uid);
            if (job == null)
            {
                return false;
            }
            job.Name = input.Name;
            job.BaseSalary = input.BaseSalary;
            _unitOfWork._jobRepo.Update(job);
            _unitOfWork.Save();
            return true;
        }
    }
}

