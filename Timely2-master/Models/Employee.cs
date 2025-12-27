using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [ForeignKey("DepartmentWorkingId")]
        public int DepartmentWorkingId { get; set; }
        public DepartmentWorking? DepartmentWorking { get; set; }


        [ForeignKey("JobId")]
        public int JobId { get; set; }
        public Job? Job { get; set; }
        

        [ForeignKey("NationalityId")]
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
