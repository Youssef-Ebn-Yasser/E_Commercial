namespace E_Commercial.Helper.Interface;

public interface ICategoryHelper
{
    public List<ShowAllCategoryViewModel>? GetShowAllViewModelList();
    public Task<List<ShowAllCategoryViewModel>?> GetCategoryContainsAsync(string search);
    public IQueryable<ShowAllCategoryViewModel>? GetAllCategoryWithProducts();
    public IQueryable<ShowAllCategoryViewModel>? GetAllCategoryWithoutProducts();
    public Task<UpdateCategoryViewModel?> FindCategoryAsync(int? id);

    public Task<bool> CreateAsync(CreateCategoryViewModel? createCategoryViewModel);

    public Task<bool> UpdateAsync(UpdateCategoryViewModel? updateCategoryViewModel);

    public Task<bool> DeleteAsync(int? id);
}