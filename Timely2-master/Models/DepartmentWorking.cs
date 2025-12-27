using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class DepartmentWorking
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }  //صباح , مساء
        public DateTime StartTime { get; set; } //  بداية الدوام
        public DateTime EndTime { get; set; }   //  نهاية الدوام
        public bool? IsActive { get; set; }
        public string Day { get; set; }
        public ICollection<Employee>? Employees { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
        public Services? Service { get; set; }


    }
}
