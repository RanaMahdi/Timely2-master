using System.ComponentModel.DataAnnotations.Schema;
using Timely.Models;

namespace Timely.Dtos
{
    public class UserDto
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public Role? Roles { get; set; }
    }
}
