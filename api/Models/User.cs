using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("User")]
    public class User {
        [Key]
        public int ID { get; set; }
        public Department Type { get; set; } = Department.None;
        [Range(0, 1E+6)]
        [Required]
        public float Salary { get; set; } = 2500;
    }

    public enum Department {
        None,
        Owner,
        Sales,
        IT,
        Marketing,
        Other
    }
}