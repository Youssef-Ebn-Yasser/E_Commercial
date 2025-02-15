namespace BusinessLayer.Services.Interface;

public interface IAccountServices
{
    public Task<bool> RegisterAsync(User user,string password);
    public Task<bool> LogInAsync(User user, string password, bool isPersistent);
    public Task LogOutAsync();
}