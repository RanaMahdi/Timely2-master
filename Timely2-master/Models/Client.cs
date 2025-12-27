using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Client
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }

        [ForeignKey("Nationality")]
        public int? NationalityId { get; set; }
        public Nationality ?Nationality { get; set; }




    }
}
