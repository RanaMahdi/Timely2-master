using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Timely.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }


        [Required]

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
