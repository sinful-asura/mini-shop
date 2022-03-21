using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models {
    [Table("WorksIn")]
    public class WorksIn {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        [ForeignKey("UserID_FK")]
        public int UserID { get; set; }
        [ForeignKey("StoreID_FK")]
        public int StoreID { get; set;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Department Type { get; set; } = Department.None;
        public DateTime StartDate { get; private set; } = new DateTime();
    }
}