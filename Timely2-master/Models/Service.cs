using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public string Name { get; set; }


        public double Price { get; set; }
        public bool IsActive { get; set; } = true;


        [ForeignKey("TypeService")]
        public int? TypeServiceId { get; set; }
        public TypeService? TypeService { get; set; } 

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<DepartmentWorking>? DepartmentWorkings { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
