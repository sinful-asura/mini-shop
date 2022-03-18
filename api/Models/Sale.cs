using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("Sale")]
    public class Sale {
        [Key]
        public int ID { get; set; }
        public User Seller { get; set; } = new User();
        public List<Item> SoldItems { get; set; } = new List<Item>();
        public DateTime SaleDate { get; set; } = new DateTime();
        public virtual string? Coupon { get; set; }
    }
}