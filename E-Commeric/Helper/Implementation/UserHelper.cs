namespace E_Commercial.Helper.Implementation;

public class UserHelper : IUserHelper
{
    #region   Fields
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    #endregion

    #region   Constructor
    public UserHelper(IUserServices userServices, IMapper mapper)
    {
        _userServices = userServices;
        _mapper = mapper;
    }
    #endregion

    #region   Handle Methods
    public async Task<List<ShowAllUsersViewModel>?> GetUserContains(string search)
    {
        var result = _userServices.GetQueryableUserTypeContainsWord(search);
        var model = _mapper.ProjectTo<ShowAllUsersViewModel>(result);

        return model is not null ? await model.ToListAsync() : null;
    }
    public IQueryable<ShowAllUsersViewModel>? GetAllClientUsers()
    {
        var usersClient = _userServices.GetAllClientUsers();

        var users = _mapper.Map<IQueryable<ShowAllUsersViewModel>>(usersClient);
        return users;
    }
    public IQueryable<ShowAllUsersViewModel>? GetAllUsersExceptClient()
    {
        var usersExceptClient = _userServices.GetAllUsersExceptClient();

        var users = _mapper.ProjectTo<ShowAllUsersViewModel>(usersExceptClient);
        return users;
    }
    public async Task<UpdateUserViewModel?> FindUserAsync(string id)
    {
        var user = await _userServices.isUserExistAsync(id);

        var result = _mapper.Map<UpdateUserViewModel>(user.Item2);

        return result;
    }

    public async Task<bool> CreateAsync(CreateUserViewModel createUserViewModel)
    {
        if (createUserViewModel is null || createUserViewModel.Password is null)
            return false;

        var user = _mapper.Map<User>(createUserViewModel);
        var result = await _userServices.CreateAsync(user, createUserViewModel.Password);

        return result;
    }

    public async Task<bool> UpdateAsync(UpdateUserViewModel updateUserViewModel)
    {
        if (updateUserViewModel is null)
            return false;
        var user = await _userServices.isUserExistAsync(updateUserViewModel.Id);

        if (!user.Item1 || user.Item2 == null)
            return false;

        var userMapped = _mapper.Map(updateUserViewModel, user.Item2);

        if (userMapped == null)
            return false;

        var result = await _userServices.UpdateAsync(userMapped);

        return result;
    }

    public async Task<bool> DeleteAsync(string? id)
    {
        if (id is null)
            return false;

        var result = await _userServices.DeleteAsync(id);

        return result;
    }

    #endregion
}