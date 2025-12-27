using Timely.Models;

namespace Timely.Interfaces
{
    public interface IUnitOfWork
    {


     
        public IAppointmentRepo _appointmentRepo { get; }
        public IAccountRepo _accountRepo { get; }
        public IClientRepo _clientRepo { get; }
        public IDepartmentRepo _departmentRepo { get; }
        public IDepartWorkingRepo _departWorkingRepo { get; }
        public IDetailsRepo _detailsRepo { get; }
        public IEmployeeRepo _employeeRepo { get; }
        public IInvoiceRepo _invoiceRepo { get; }
        public IJobRepo _jobRepo { get; }
        public INationalityRepo _nationalityRepo { get; }
        public IPaymentRepo _paymentRepo { get; }
        public IReviewRepo _reviewRepo { get; }
        public IRoleRepo _roleRepo { get; }
        public IServiceRepo _serviceRepo { get; }
        public ITypeServiceRepo _typeServiceRepo { get; }
        public IRepository<User> _repositoryUsers { get; }



        void Save();
    }
}
