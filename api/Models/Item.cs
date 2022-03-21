using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Item")]
    public class Item {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        [MaxLength(255)]
        public string Name { get; set; } = "ITEM_NAME_UNSET";
        [Required]
        [Range(0, 1E+03)]
        public float Price { get; set; } = 0;

        [ForeignKey("BelongsToSectionFK")]
        public Section? Section { get; set; }
        [Range(0, 100)]
        public virtual float Discount { get; set; } = 0;
    }
}