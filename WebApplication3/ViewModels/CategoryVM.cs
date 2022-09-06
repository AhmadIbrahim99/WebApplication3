using System.Collections.Generic;

namespace WebApplication3.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubCategoryVM> Subcategories { get; set; }
    }
}
