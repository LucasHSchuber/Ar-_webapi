using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Aråstock.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        [Required]
        public string? UnitName { get; set; }
    }
}
