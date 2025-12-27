using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Account;
using Timely.Interfaces;
using Timely.Models;

namespace Timely.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal IEnumerable<Account> Account;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

        //  المستخدمون والأدوار

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        //  الموظفون والإدارات

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentWorking> DepartmentWorkings { get; set; }
        public DbSet<Job> Jobs { get; set; }

        //  العملاء والمواعيد

        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
   

        //  الخدمات وأنواعها

        public DbSet<Services> Services { get; set; }
        public DbSet<TypeService> TypeServices { get; set; }

        //  الجنسيات والمدفوعات

        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Payment> Payments { get; set; }


        //  المراجعات والتفاصيل

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Detail> Details  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "Syrian" },
                new Nationality { Id = 2, Name = "Saudi"},
                new Nationality { Id = 3, Name = "Egyptian"},
                new Nationality { Id = 4, Name = "Jordanian"},
                new Nationality { Id = 5, Name = "Lebanese"},
                new Nationality { Id = 6, Name = "Iraqi"},
                new Nationality { Id = 7, Name = "Yemeni"},
                new Nationality { Id = 8, Name = "Kuwaiti" },
                new Nationality { Id = 9, Name = "Emirati"},
                new Nationality { Id = 10, Name = "Qatari"},
                new Nationality { Id = 11, Name = "Omani"},
                new Nationality { Id = 12, Name = "Bahraini" },
                new Nationality { Id = 13, Name = "Sudanese"},
                new Nationality { Id = 14, Name = "Moroccan"},
                new Nationality { Id = 15, Name = "Algerian"},
                new Nationality { Id = 16, Name = "Tunisian" },
                new Nationality { Id = 17, Name = "Libyan"},
                new Nationality { Id = 18, Name = "Palestinian"},
                new Nationality { Id = 19, Name = "Turkish"},
                new Nationality { Id = 20, Name = "Iranian"},
                new Nationality { Id = 21, Name = "American"},
                new Nationality { Id = 22, Name = "British"},
                new Nationality { Id = 23, Name = "French"}
            );

            modelBuilder.Entity<User>().HasData(
            new User
            {
            Id = 1,
            Name = "Ahmed Ali",
            Phone = "0551234567",
            Password = "123456",
            Email = "ahmed.ali@example.com",
            RoleId = 1
            },
            new User
            {
            Id = 2,
            Name = "Sara Mohammed",
            Phone = "0569876543",
            Password = "123456",
            Email = "sara.mohammed@example.com",
            RoleId = 2
            },
            new User
            {
            Id = 3,
            Name = "Omar Nasser",
            Phone = "0571122334",
            Password = "123456",
            Email = "omar.nasser@example.com",
            RoleId = 1
            },
            new User
            {
            Id = 4,
            Name = "Laila Hassan",
            Phone = "0583344556",
            Password = "123456",
            Email = "laila.hassan@example.com",
            RoleId = 3
            },
            new User
            {
            Id = 5,
            Name = "Khalid Al-Faisal",
            Phone = "0596677889",
            Password = "123456",
            Email = "khalid.faisal@example.com",
            RoleId = 2
            }
            );

            modelBuilder.Entity<Role>().HasData(
             new Role
             {
                 Id = 1,
                 Uid = "e12a4b15-4b8d-4b2a-bc9b-0f1a3b2f3a11",
                 CreatedAt = new DateTime(2024, 10, 1, 12, 0, 0),
                 Name = "Admin",
                 Permission = "FullAccess"
             },
             new Role
             {
                 Id = 2,
                 Uid = "a47b9e72-3a92-44d2-8a0c-5a1d4b8e7a22",
                 CreatedAt = new DateTime(2024, 10, 1, 12, 0, 0),
                 Name = "Manager",
                 Permission = "ManageUsers,ViewReports"
             },
             new Role
             {
                 Id = 3,
                 Uid = "b92d5e64-1f23-4c91-96f8-6b2e4d9c8c33",
                 CreatedAt = new DateTime(2024, 10, 1, 12, 0, 0),
                 Name = "User",
                 Permission = "ViewOwnData"
             }
         );


            modelBuilder.Entity<Employee>().HasData(
    new Employee
    {
        Id = 1,
        Uid = "c8a25f76-1b4a-4d9b-9b84-1a6c4e8b8f11",
        CreatedAt = new DateTime(2024, 10, 1, 10, 0, 0),
        Name = "Ahmed Ali",
        Phone = "+966512345678",
        Password = "123",
        Email = "ahmed.ali@example.com",
        DepartmentId = 1,
        DepartmentWorkingId = 1,
        JobId = 1,
        NationalityId = 1
    },
    new Employee
    {
        Id = 2,
        Uid = "a5f3b9d2-4f6c-4d89-a317-9c9a8c6d9b22",
        CreatedAt = new DateTime(2024, 10, 1, 10, 0, 0),
        Name = "Sara Mohammed",
        Phone = "+966523456789",
        Password = "123",
        Email = "sara.mohammed@example.com",
        DepartmentId = 2,
        DepartmentWorkingId = 1,
        JobId = 2,
        NationalityId = 2
    },
    new Employee
    {
        Id = 3,
        Uid = "f7b4d2a9-87cd-42a1-93c2-12e4a9e6d4c3",
        CreatedAt = new DateTime(2024, 10, 1, 10, 0, 0),
        Name = "Khalid Al-Faisal",
        Phone = "+966534567890",
        Password = "123",
        Email = "khalid.faisal@example.com",
        DepartmentId = 1,
        DepartmentWorkingId = 2,
        JobId = 3,
        NationalityId = 3
    },
    new Employee
    {
        Id = 4,
        Uid = "e9d5c2a7-4b2e-46f8-a1c9-5f9a3e2d1b44",
        CreatedAt = new DateTime(2024, 10, 1, 10, 0, 0),
        Name = "Laila Hassan",
        Phone = "+966545678901",
        Password = "123",
        Email = "laila.hassan@example.com",
        DepartmentId = 3,
        DepartmentWorkingId = 2,
        JobId = 4,
        NationalityId = 4
    },
    new Employee
    {
        Id = 5,
        Uid = "b3f9a8c6-9d4e-42f3-bc9a-1e7a2c6f8b55",
        CreatedAt = new DateTime(2024, 10, 1, 10, 0, 0),
        Name = "Omar Nasser",
        Phone = "+966556789012",
        Password = "123",
        Email = "omar.nasser@example.com",
        DepartmentId = 1,
        DepartmentWorkingId = 3,
        JobId = 5,
        NationalityId = 5
    }
);


            // ---------------------- Departments ----------------------
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Uid = "d7b4e4a6-4c9a-4f21-9a33-1f2b7d8e9a01",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "IT Department",
                    Description = "Handles all technical and IT-related tasks"
                },
                new Department
                {
                    Id = 2,
                    Uid = "b3c1d8f7-5a8e-4f2a-8c33-9f1a2b7d5e02",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "HR Department",
                    Description = "Responsible for employee relations and recruitment"
                },
                new Department
                {
                    Id = 3,
                    Uid = "e9f6a1c2-2b8a-42b1-bd3f-5c9a3f2d8a03",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Finance Department",
                    Description = "Manages budgets, payroll, and accounting"
                }
            );


            // ---------------------- DepartmentWorking ----------------------
            modelBuilder.Entity<DepartmentWorking>().HasData(
                new DepartmentWorking
                {
                    Id = 1,
                    Uid = "a1b2c3d4-e5f6-7890-ab12-cd34ef56a101",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Morning Shift",
                    StartTime = new DateTime(2024, 10, 1, 8, 0, 0),
                    EndTime = new DateTime(2024, 10, 1, 16, 0, 0),
                    IsActive = true,
                    Day = "Sunday",
                    DepartmentId = 1
                },
                new DepartmentWorking
                {
                    Id = 2,
                    Uid = "b2c3d4e5-f6a7-8901-bc23-de45f678b202",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Evening Shift",
                    StartTime = new DateTime(2024, 10, 1, 16, 0, 0),
                    EndTime = new DateTime(2024, 10, 1, 23, 0, 0),
                    IsActive = true,
                    Day = "Sunday",
                    DepartmentId = 1
                },
                new DepartmentWorking
                {
                    Id = 3,
                    Uid = "c3d4e5f6-a7b8-9012-cd34-ef56a789c303",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Morning Shift",
                    StartTime = new DateTime(2024, 10, 1, 9, 0, 0),
                    EndTime = new DateTime(2024, 10, 1, 17, 0, 0),
                    IsActive = true,
                    Day = "Monday",
                    DepartmentId = 2
                }
            );


            // ---------------------- Jobs ----------------------
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = 1,
                    Uid = "d4e5f6a7-b8c9-0123-de45-f678a9bcd401",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Software Engineer",
                    BaseSalary = 8500
                },
                new Job
                {
                    Id = 2,
                    Uid = "e5f6a7b8-c9d0-1234-ef56-a789bcd0e502",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "HR Specialist",
                    BaseSalary = 7000
                },
                new Job
                {
                    Id = 3,
                    Uid = "f6a7b8c9-d0e1-2345-f678-b9cd0e1f6303",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Project Manager",
                    BaseSalary = 12000
                },
                new Job
                {
                    Id = 4,
                    Uid = "a7b8c9d0-e1f2-3456-a789-cd0e1f2a7404",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Accountant",
                    BaseSalary = 6500
                },
                new Job
                {
                    Id = 5,
                    Uid = "b8c9d0e1-f2a3-4567-b89a-d0e1f2a3b505",
                    CreatedAt = new DateTime(2024, 10, 1, 9, 0, 0),
                    Name = "Graphic Designer",
                    BaseSalary = 6000
                }
            );





        }

    }
}
