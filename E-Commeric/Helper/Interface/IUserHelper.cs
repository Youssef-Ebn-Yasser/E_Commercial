namespace E_Commercial.Helper.Interface;

public interface IUserHelper
{
    public IQueryable<ShowAllUsersViewModel>? GetAllUsersExceptClient();
    public IQueryable<ShowAllUsersViewModel>? GetAllClientUsers();
    public Task<List<ShowAllUsersViewModel>?> GetUserContains(string search);
    public Task<UpdateUserViewModel?> FindUserAsync(string id);

    public Task<bool> CreateAsync(CreateUserViewModel createUserViewModel);
    public Task<bool> UpdateAsync(UpdateUserViewModel updateUserViewModel);
    public Task<bool> DeleteAsync(string id);
}