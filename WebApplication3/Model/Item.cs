using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3.Model
{
    public partial class Item 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }
        public DateTime CraetedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int SubcategoryId { get; set; }

        public virtual Subcategory Subcategory { get; set; }
    }
}
