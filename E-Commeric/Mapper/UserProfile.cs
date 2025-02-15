namespace E_Commercial.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ShowAllUsersViewModel>();
        CreateMap<User, UpdateUserViewModel>().ReverseMap();
        CreateMap<CreateUserViewModel, User>().ReverseMap();

    }
}