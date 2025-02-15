namespace E_Commercial.Mapper;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterViewModel, User>();
        CreateMap<LoginViewModel, User>();
    }
}