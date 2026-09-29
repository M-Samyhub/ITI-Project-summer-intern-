using AutoMapper;
using ItiFinalProject.Models;
using ItiFinalProject.View_Model.Category;
using ItiFinalProject.View_Model.Product;

namespace ItiFinalProject.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryViewModel>()
            .ForMember(dest => dest.ProductCount,
                       opt => opt.MapFrom(src => src.Products != null ? src.Products.Count : 0));
            CreateMap<CreateCategoryViewModel, Category>();
            CreateMap<UpdateCategoryViewModel, Category>();
            CreateMap<Category, UpdateCategoryViewModel>();
            

            //Product Mappings
            CreateMap<Product,ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
            CreateMap<CreateProductViewModel, Product>();
            CreateMap<UpdateProductViewModel, Product>();
            CreateMap<Product, UpdateProductViewModel>()
                .ForMember(dest => dest.ImgePath, opt => opt.Ignore())
                .ForMember(dest => dest.ExistingImagePath, opt => opt.MapFrom(src => src.ImgePath));

        }
    }
}
