namespace E_Commercial.Mapper;

public class CategoryTypeProfile : Profile
{
    public CategoryTypeProfile()
    {
        CreateMap<CategoryType, ShowAllCategoryTypeViewModel>()
        .ForMember(des => des.NumberOfCategory
        , ct => ct.MapFrom(des => des.Categories!.Count));
        CreateMap<CreateCategoryTypeViewModel, CategoryType>().ReverseMap();
        CreateMap<CategoryType, UpdateCategoryTypeViewModel>().ReverseMap();
    }
}