using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeptApi.Models
{
    public class Dept
    {
        [Key]
        public int DeptId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        [Range(1, 10000)]
        public int NumOfEmployees { get; set; }

        // Foreign key for Manager
        public int? ManagerId { get; set; }
        public Manager? Manager { get; set; }
    }
}
