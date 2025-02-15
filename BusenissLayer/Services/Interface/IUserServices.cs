namespace BusinessLayer.Services.Interface;

public interface IUserServices
{
    public IQueryable<User>? GetAllUsersExceptClient();
    public IQueryable<User>? GetAllClientUsers();
    public IQueryable<User?>? GetQueryableUserTypeContainsWord(string? search);
    public Task<bool> CreateAsync(User user, string password);
    public Task<bool> UpdateAsync(User user);
    public Task<bool> DeleteAsync(string id);
    public Task<(bool, User?)> isUserExistAsync(string id);
}