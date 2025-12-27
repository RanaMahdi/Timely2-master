using Timely.Data;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _appointmentRepo = new AppointmentRepo(_context);
           // _accountRepo = new AccountRepo(_context);
            _clientRepo = new ClientRepo(_context);
            _departmentRepo = new DepartmentRepo(_context); 
            _departWorkingRepo = new DepartWorkingRepo(_context);
            _detailsRepo = new DetailsRepo(_context);
            _employeeRepo = new EmployeeRepo(_context);
            _nationalityRepo = new NationalityRepo(_context);
            _paymentRepo = new PaymentRepo(_context);
            _invoiceRepo = new InvoiceRepo(_context);
            _jobRepo = new JobRepo(_context);
            _reviewRepo = new ReviewRepo(_context);
            _roleRepo = new RoleRepo(_context);
            _serviceRepo = new ServiceRepo(_context);
            _typeServiceRepo = new TypeServiceRepo(_context);
            _repositoryUsers = new MainRepository<User>(_context);
        }

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



            public void Save()
            {
                _context.SaveChanges();
            }
        }
    }
    