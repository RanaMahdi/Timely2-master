using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Services>? Services { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<DepartmentWorking>? DepartmentWorkings { get; set; }
    }
}
