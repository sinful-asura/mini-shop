using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("WorksIn")]
    public class WorksIn {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public User User { get; set; } = null!;
        public Store Store { get; set;} = null!;
        public DateTime StartDate { get; set; } = new DateTime();
    }
}