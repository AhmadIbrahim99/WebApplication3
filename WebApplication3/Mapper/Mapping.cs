using AutoMapper;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Subcategory, SubCategoryVM>().ReverseMap();
            CreateMap<Item, ItemVM>().
                ForMember(a => a.SubCategory, a => a.
                MapFrom(x => x.Subcategory.Name)).
                ForMember(a => a.Category, a => a.
                MapFrom(x => x.Subcategory.Category.Name)).ReverseMap();
        }
    }
}
