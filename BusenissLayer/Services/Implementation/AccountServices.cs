namespace BusinessLayer.Services.Implementation;

public class AccountServices : IAccountServices
{
    #region   Fields
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    #endregion

    #region   Constructor
    public AccountServices(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    #endregion

    #region   Handle Method
    public async Task<bool> RegisterAsync(User user, string password)
    {
        if (user.Email == null)
            return false;

        var isExist = await _userManager.FindByEmailAsync(user.Email);
        if (isExist != null)
            return false;

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return false;

        await _signInManager.SignInAsync(user, isPersistent: true);
        return true;
    }
    public async Task<bool> LogInAsync(User user, string password, bool isPersistent)
    {
        if (user is null || user.Email is null)
            return false;

        var isEmailValid = new EmailAddressAttribute().IsValid(user.Email);

        var ExistUser = isEmailValid ? await _userManager.FindByEmailAsync(user.Email) :
                          user.UserName is null ? null : await _userManager.FindByNameAsync(user.UserName);

        var validPassword = await _userManager.CheckPasswordAsync(ExistUser!, password);

        if (validPassword == false)
            return false;

        var result = await _signInManager.PasswordSignInAsync(ExistUser!, password, isPersistent, false);

        return result.Succeeded ? true : false;
    }
    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    #endregion
}