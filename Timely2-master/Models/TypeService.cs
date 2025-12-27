namespace Timely.Models
{
    public class TypeService
    {
       public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Services> Services { get; set; }  

    }
}
