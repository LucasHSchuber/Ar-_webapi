using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Aråstock.Models
{

    public class Category
    {
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }

        //navigation property
        public virtual ICollection<Item>? items { get; set; }
    }
}