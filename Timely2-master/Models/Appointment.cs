using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
        public ICollection<Detail>? Details { get; set; }

        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
