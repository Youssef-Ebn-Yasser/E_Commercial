namespace E_Commercial.Helper.Implementation;

public class AccountHelper : IAccountHelper
{
    #region   Fields
    private readonly IMapper _mapper;
    private readonly IAccountServices _accountServices;
    #endregion

    #region   Constructor
    public AccountHelper(IMapper mapper, IAccountServices accountServices)
    {
        _mapper = mapper;
        _accountServices = accountServices;
    }
    #endregion

    #region   Handle Methods
    public async Task<bool> RegisterAsync(RegisterViewModel model)
    {
        var user = _mapper.Map<User>(model);

        var result = await _accountServices.RegisterAsync(user, model.Password);

        return result;
    }
    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        var user = _mapper.Map<User>(model);

        var result = await _accountServices.LogInAsync(user, model.Password, model.RememberMe);

        return result;
    }
    public async Task LogOutAsync()
    {
        await _accountServices.LogOutAsync();
    }
    #endregion
}