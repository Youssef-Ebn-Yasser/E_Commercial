namespace E_Commercial.Helper.Interface;

public interface IAccountHelper
{
    public Task<bool> RegisterAsync(RegisterViewModel model);
    public Task<bool> LoginAsync(LoginViewModel model);
    public Task LogOutAsync();
}