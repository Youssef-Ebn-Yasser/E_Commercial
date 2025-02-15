namespace E_Commeric.Mapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ShowAllProductsViewModel>().ForMember(des => des.CategoryName,
        f => f.MapFrom(src => src.Category!.CategoryName))
                                                                       .ForMember(dest => dest.Paths,
               opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.Path).ToList()));

        CreateMap<CreateProductViewModel, Product>();
        CreateMap<UpdateProductViewModel, Product>().ReverseMap();
        CreateMap<Product, ShowProductDetails>().ForMember(des => des.Paths, opt => opt.MapFrom(src => src.ProductImages.Select(img => img.Path)));
    }
}