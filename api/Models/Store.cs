using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models {
    [Table("Store")]
    public class Store {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = "STORE_NAME_UNSET";
        [JsonIgnore]
        public List<WorksIn>? Staff { get; set; } = new List<WorksIn>();
    }
}