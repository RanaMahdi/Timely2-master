namespace Timely.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Uid { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Permission { get; set; }
  

        public ICollection<User>? Users { get; set; }
    }
}
