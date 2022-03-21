using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Section")]
    public class Section {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = "SECTION_NAME_UNSET";
        public List<Item> Items { get; set; } = new List<Item>();
    }
}