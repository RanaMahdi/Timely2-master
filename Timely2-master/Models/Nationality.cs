using System.ComponentModel.DataAnnotations.Schema;

namespace Timely.Models
{
    public class Nationality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }


        public ICollection<Employee>? Employees { get; set; }
        public ICollection<Client>? Clients { get; set; }
      


    }
}
