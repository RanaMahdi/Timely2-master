namespace Timely.Dtos
{
    public class AppointmentDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ClientId { get; set; }
        public int? EmployeeId { get; set; }


    }
}
