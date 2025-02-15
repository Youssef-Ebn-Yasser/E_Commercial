namespace BusinessLayer.Services.Implementation;

public class UserServices : IUserServices
{
    #region   Fields
    private readonly UserManager<User> _userManager;
    #endregion

    #region   Constructor
    public UserServices(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    #endregion

    #region   Handle Methods
    public IQueryable<User> GetAllClientUsers()
    {
        var userClients = _userManager.Users.AsNoTracking().Where(u => u.Type == "Client");

        return userClients;
    }
    public IQueryable<User> GetAllUsersExceptClient()
    {
        var userClients = _userManager.Users.AsNoTracking().Where(u => u.Type != "Client");

        return userClients;
    }
    public IQueryable<User?>? GetQueryableUserTypeContainsWord(string? search)
    {
        var result = _userManager.Users.Where(u => u.UserName!.Contains(search!) && u.Type != "Client");

        return result;
    }

    public async Task<bool> CreateAsync(User user, string password)
    {
        IdentityResult result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
            return true;

        return false;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var results = await _userManager.UpdateAsync(user);

        if (results.Succeeded)
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await isUserExistAsync(id);
        if (!result.Item1 || result.Item2 is null)
            return false;

        var results = await _userManager.DeleteAsync(result.Item2);

        if (results.Succeeded)
            return true;

        return false;
    }

    public async Task<(bool, User?)> isUserExistAsync(string id)
    {
        var userExist = await _userManager.FindByIdAsync(id);

        return userExist == null ? (false, null) : (true, userExist);
    }
    #endregion
}