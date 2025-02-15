namespace E_Commercial.Helper.Interface;

public interface ICategoryTypeHelper
{
    public IQueryable<ShowAllCategoryTypeViewModel>? GetAllCategoryTypeWithCategories();
    public IQueryable<ShowAllCategoryTypeViewModel>? GetAllCategoryTypeWithoutCategories();
    public Task<List<ShowAllCategoryTypeViewModel>?> GetCategoryTypeContainsAsync(string search);

    public Task<UpdateCategoryTypeViewModel?> FindCategoryTypeAsync(int? id);
    public Task<bool> CreateAsync(CreateCategoryTypeViewModel? createCategoryTypeViewModel);
    public Task<bool> UpdateAsync(UpdateCategoryTypeViewModel? updateCategoryTypeViewModel);
    public Task<bool> DeleteAsync(int? id);
}