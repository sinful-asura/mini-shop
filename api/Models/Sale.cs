using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Sale")]
    public class Sale {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }
        public User Seller { get; set; } = null!;
        public List<Item> SoldItems { get; set; } = new List<Item>();
        public DateTime SaleDate { get; set; } = new DateTime();
        public virtual string Coupon { get; set; } = string.Empty;
    }
}