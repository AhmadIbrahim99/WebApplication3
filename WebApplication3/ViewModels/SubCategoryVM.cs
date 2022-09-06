using System.Collections.Generic;

namespace WebApplication3.ViewModels
{
    public class SubCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryVM Category { get; set; }
        public virtual ICollection<ItemVM> Items { get; set; }
    }
}
