using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Comments { get; set; }

        [Range(1, 5)]
        [Required]
        public double Rating { get; set; } 
        public DateTime ReviewDate { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public  Services?  service { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
}
}
