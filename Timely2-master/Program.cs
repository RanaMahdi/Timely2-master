using Microsoft.EntityFrameworkCore;
using Timely.Data;
using Timely.Interfaces;
using Timely.Interfaces.IServices;
using Timely.Repositories;
using Timely.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(builder =>
{
    builder.IdleTimeout = TimeSpan.FromMinutes(30);
    builder.Cookie.HttpOnly = true;
    builder.Cookie.IsEssential = true;
});

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen();

// section 1: Configure Entity Framework and SQL Server
// file AppSettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// file ApplicationDbContext.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(MainRepository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

// Register repositories
builder.Services.AddScoped(typeof(IAppointmentRepo), typeof(AppointmentRepo));
builder.Services.AddScoped(typeof(IAccountRepo), typeof(AccountRepo));
builder.Services.AddScoped(typeof(IClientRepo), typeof(ClientRepo));
builder.Services.AddScoped(typeof(IDepartmentRepo), typeof(DepartmentRepo));
builder.Services.AddScoped(typeof(IDepartWorkingRepo), typeof(DepartWorkingRepo));
builder.Services.AddScoped(typeof(IDetailsRepo), typeof(DetailsRepo));
builder.Services.AddScoped(typeof(IEmployeeRepo), typeof(EmployeeRepo));
builder.Services.AddScoped(typeof(IInvoiceRepo), typeof(InvoiceRepo));
builder.Services.AddScoped(typeof(IJobRepo), typeof(JobRepo));
builder.Services.AddScoped(typeof(INationalityRepo), typeof(NationalityRepo));
builder.Services.AddScoped(typeof(IPaymentRepo), typeof(PaymentRepo));
builder.Services.AddScoped(typeof(IReviewRepo), typeof(ReviewRepo));
builder.Services.AddScoped(typeof(IRoleRepo), typeof(RoleRepo));
builder.Services.AddScoped(typeof(IServiceRepo), typeof(ServiceRepo));
builder.Services.AddScoped(typeof(ITypeServiceRepo), typeof(TypeServiceRepo));

// Register services
builder.Services.AddScoped(typeof(IAppointmentServices), typeof(AppointmentService));
builder.Services.AddScoped(typeof(IAccountServices), typeof(AccountService));
builder.Services.AddScoped(typeof(IClientServices), typeof(ClientService));
builder.Services.AddScoped(typeof(IDepartmentServices), typeof(DepartmentService));
builder.Services.AddScoped(typeof(IDepartWorkServices), typeof(DepartWorkService));
builder.Services.AddScoped(typeof(IDetailServices), typeof(DetailService));
builder.Services.AddScoped(typeof(IEmployeeServices), typeof(EmployeeService));
builder.Services.AddScoped(typeof(IInvoiceServices), typeof(InvoiceService));
builder.Services.AddScoped(typeof(IJobServices), typeof(JobService));
builder.Services.AddScoped(typeof(INationalityServices), typeof(NationalityService));
builder.Services.AddScoped(typeof(IPaymentServices), typeof(PaymentService));
builder.Services.AddScoped(typeof(IReviewServices), typeof(ReviewService));
builder.Services.AddScoped(typeof(IRoleServices), typeof(RoleService));
builder.Services.AddScoped(typeof(IServicesServices), typeof(ServicesService));
builder.Services.AddScoped(typeof(ITypeServiceServices), typeof(TypeServiceService));
builder.Services.AddScoped(typeof(IUserServices), typeof(UserService));


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timely API V1");
    c.RoutePrefix = "swagger";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();