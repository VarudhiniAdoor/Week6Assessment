using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeptApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
