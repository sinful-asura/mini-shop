using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models {
    [Table("User")]
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        public string Name { get; set; } = string.Empty;
        [Range(0, 1E+6)]
        [Required]
        public float Salary { get; set; } = 0;
        [JsonIgnore]
        public List<WorksIn>? Workplaces { get; set; } = new List<WorksIn>();
    }
}