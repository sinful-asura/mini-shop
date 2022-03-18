using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Store")]
    public class Store {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = "STORE_NAME_UNSET";

        public List<User> Staff { get; set; } = new List<User>();
    }
}