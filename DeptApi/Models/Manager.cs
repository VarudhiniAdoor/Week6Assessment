using System.ComponentModel.DataAnnotations;

namespace DeptApi.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public ICollection<Dept>? Departments { get; set; }
    }
}
