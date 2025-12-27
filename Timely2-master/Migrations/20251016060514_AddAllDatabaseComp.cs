using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Timely.Migrations
{
    /// <inheritdoc />
    public partial class AddAllDatabaseComp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSalary = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TypeServiceId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_TypeServices_TypeServiceId",
                        column: x => x.TypeServiceId,
                        principalTable: "TypeServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentWorkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentWorkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentWorkings_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentWorkings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentWorkingId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_DepartmentWorkings_DepartmentWorkingId",
                        column: x => x.DepartmentWorkingId,
                        principalTable: "DepartmentWorkings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    IdInvoice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.IdInvoice);
                    table.ForeignKey(
                        name: "FK_Invoices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Uid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Handles all technical and IT-related tasks", "IT Department", "d7b4e4a6-4c9a-4f21-9a33-1f2b7d8e9a01" },
                    { 2, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Responsible for employee relations and recruitment", "HR Department", "b3c1d8f7-5a8e-4f2a-8c33-9f1a2b7d5e02" },
                    { 3, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Manages budgets, payroll, and accounting", "Finance Department", "e9f6a1c2-2b8a-42b1-bd3f-5c9a3f2d8a03" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "BaseSalary", "CreatedAt", "Name", "Uid" },
                values: new object[,]
                {
                    { 1, 8500.0, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Software Engineer", "d4e5f6a7-b8c9-0123-de45-f678a9bcd401" },
                    { 2, 7000.0, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "HR Specialist", "e5f6a7b8-c9d0-1234-ef56-a789bcd0e502" },
                    { 3, 12000.0, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Project Manager", "f6a7b8c9-d0e1-2345-f678-b9cd0e1f6303" },
                    { 4, 6500.0, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Accountant", "a7b8c9d0-e1f2-3456-a789-cd0e1f2a7404" },
                    { 5, 6000.0, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Graphic Designer", "b8c9d0e1-f2a3-4567-b89a-d0e1f2a3b505" }
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Syrian" },
                    { 2, null, "Saudi" },
                    { 3, null, "Egyptian" },
                    { 4, null, "Jordanian" },
                    { 5, null, "Lebanese" },
                    { 6, null, "Iraqi" },
                    { 7, null, "Yemeni" },
                    { 8, null, "Kuwaiti" },
                    { 9, null, "Emirati" },
                    { 10, null, "Qatari" },
                    { 11, null, "Omani" },
                    { 12, null, "Bahraini" },
                    { 13, null, "Sudanese" },
                    { 14, null, "Moroccan" },
                    { 15, null, "Algerian" },
                    { 16, null, "Tunisian" },
                    { 17, null, "Libyan" },
                    { 18, null, "Palestinian" },
                    { 19, null, "Turkish" },
                    { 20, null, "Iranian" },
                    { 21, null, "American" },
                    { 22, null, "British" },
                    { 23, null, "French" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "Permission", "Uid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "FullAccess", "e12a4b15-4b8d-4b2a-bc9b-0f1a3b2f3a11" },
                    { 2, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Manager", "ManageUsers,ViewReports", "a47b9e72-3a92-44d2-8a0c-5a1d4b8e7a22" },
                    { 3, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "User", "ViewOwnData", "b92d5e64-1f23-4c91-96f8-6b2e4d9c8c33" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentWorkings",
                columns: new[] { "Id", "CreatedAt", "Day", "DepartmentId", "EndTime", "IsActive", "Name", "ServiceId", "StartTime", "Uid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Sunday", 1, new DateTime(2024, 10, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), true, "Morning Shift", null, new DateTime(2024, 10, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "a1b2c3d4-e5f6-7890-ab12-cd34ef56a101" },
                    { 2, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Sunday", 1, new DateTime(2024, 10, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), true, "Evening Shift", null, new DateTime(2024, 10, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), "b2c3d4e5-f6a7-8901-bc23-de45f678b202" },
                    { 3, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "Monday", 2, new DateTime(2024, 10, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), true, "Morning Shift", null, new DateTime(2024, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), "c3d4e5f6-a7b8-9012-cd34-ef56a789c303" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "RoleId" },
                values: new object[,]
                {
                    { 1, "ahmed.ali@example.com", "Ahmed Ali", "123456", "0551234567", 1 },
                    { 2, "sara.mohammed@example.com", "Sara Mohammed", "123456", "0569876543", 2 },
                    { 3, "omar.nasser@example.com", "Omar Nasser", "123456", "0571122334", 1 },
                    { 4, "laila.hassan@example.com", "Laila Hassan", "123456", "0583344556", 3 },
                    { 5, "khalid.faisal@example.com", "Khalid Al-Faisal", "123456", "0596677889", 2 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "DepartmentId", "DepartmentWorkingId", "Email", "JobId", "Name", "NationalityId", "Password", "Phone", "Uid" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "ahmed.ali@example.com", 1, "Ahmed Ali", 1, "123", "+966512345678", "c8a25f76-1b4a-4d9b-9b84-1a6c4e8b8f11" },
                    { 2, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "sara.mohammed@example.com", 2, "Sara Mohammed", 2, "123", "+966523456789", "a5f3b9d2-4f6c-4d89-a317-9c9a8c6d9b22" },
                    { 3, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "khalid.faisal@example.com", 3, "Khalid Al-Faisal", 3, "123", "+966534567890", "f7b4d2a9-87cd-42a1-93c2-12e4a9e6d4c3" },
                    { 4, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "laila.hassan@example.com", 4, "Laila Hassan", 4, "123", "+966545678901", "e9d5c2a7-4b2e-46f8-a1c9-5f9a3e2d1b44" },
                    { 5, new DateTime(2024, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "omar.nasser@example.com", 5, "Omar Nasser", 5, "123", "+966556789012", "b3f9a8c6-9d4e-42f3-bc9a-1e7a2c6f8b55" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_NationalityId",
                table: "Clients",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentWorkings_DepartmentId",
                table: "DepartmentWorkings",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentWorkings_ServiceId",
                table: "DepartmentWorkings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_AppointmentId",
                table: "Details",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentWorkingId",
                table: "Employees",
                column: "DepartmentWorkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AppointmentId",
                table: "Invoices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentId",
                table: "Invoices",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppointmentId",
                table: "Payments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ServiceId",
                table: "Reviews",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId",
                table: "Services",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TypeServiceId",
                table: "Services",
                column: "TypeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "DepartmentWorkings");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "TypeServices");
        }
    }
}
