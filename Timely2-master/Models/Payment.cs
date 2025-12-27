using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }

        [ForeignKey("Appointment")]         
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; } 
        public Client? Client { get; set; }

        

    }
}
