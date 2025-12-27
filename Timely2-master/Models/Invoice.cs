using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime InvoiceDate { get; set; }

        public double TotalAmount { get; set; }
        public bool IsPaid { get; set; }

        [ForeignKey("Appointment")]
        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        [ForeignKey("Client")]
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
