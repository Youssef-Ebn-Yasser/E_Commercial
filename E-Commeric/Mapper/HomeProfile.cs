namespace E_Commercial.Mapper;

public class HomeProfile : Profile
{
    public HomeProfile()
    {
        CreateMap<CategoryType, ShowCategoryTypeWithAllRelated>();
        CreateMap<Category, CategoryViewModel>();
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductImage, ProductImageViewModel>();
        CreateMap<Product, CartPageViewModel>();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.ProductImages.Any() ? src.ProductImages.FirstOrDefault().Path : "/images/default.jpg"))
            .ReverseMap();

        CreateMap<List<ProductDto>, CartPageViewModel>()
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Sum(p => p.Price * p.Amount)))
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Sum(p => p.Amount)))
            .ForMember(dest => dest.AfterDiscount, opt => opt.MapFrom((src, dest) => dest.TotalPrice - (dest.TotalPrice * dest.Discount / 100)))
            .ForMember(dest => dest.Tax, opt => opt.MapFrom((src, dest) => dest.TotalPrice * dest.Discount))

             .ForMember(dest => dest.productDto, opt => opt.MapFrom(src => src));

        CreateMap<CartPageViewModel, Order>();
        CreateMap<CartPageViewModel, ProductOrder>()
        .ForMember(des => des.ProductId, opt => opt.MapFrom(src => src.productDto!.Select(pdt => pdt.ProductId)))
        .ForMember(des => des.OrderId, opt => opt.MapFrom(src => src.OrderId));



    }
}