namespace E_Commercial.Mapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
        CreateMap<ShowAllCategoryViewModel, Category>().ReverseMap();
        CreateMap<UpdateCategoryViewModel, Category>().ReverseMap();

    }
}