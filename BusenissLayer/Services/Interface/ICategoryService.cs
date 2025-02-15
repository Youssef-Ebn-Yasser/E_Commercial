namespace BusinessLayer.Services.Interface;

public interface ICategoryService
{
    public Task<List<Category>?> GetCategoryListIncludingProductsAsync();
    public Task<(bool, Category?)> GetByIdAsync(int? id);
    public IQueryable<Category?>? GetCategoriesAsQueryableIncludingProducts();
    public IQueryable<Category?>? GetQueryableCategoryContainsWord(string? search);
    public IQueryable<Category?>? GetCategoriesAsQueryableWithoutProducts();
    public Task<bool> CreateAsync(Category? category);
    public Task<bool> UpdateAsync(Category? category);
    public Task<bool> DeleteAsync(int? id);
}