using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Aråstock.Models
{

    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public string? ItemName { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }
        // public string? CategoryName { get; set; } // Property to hold the CategoryName


        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitID { get; set; }

        [Required]
        public int Amount { get; set; }

        [ForeignKey("UnitID")]
        public virtual Unit? Unit { get; set; }


        public DateTime Created { get; set; }


        // Calculated property to get the total amount in stock
        [NotMapped] // This property won't be mapped to the database
        public int TotalAmountInStock => Quantity * Amount;
    }
}