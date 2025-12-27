namespace Timely.Dtos
{
    public class DepartmentWorkingDto
    {
        public string Name { get; set; } 
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }  
        public bool? IsActive { get; set; }
        public string Day { get; set; }
        public int? DepartmentId { get; set; }
        public int? ServiceId { get; set; }

    }
}
