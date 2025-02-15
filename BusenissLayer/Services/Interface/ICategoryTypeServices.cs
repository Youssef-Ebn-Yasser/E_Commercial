namespace BusinessLayer.Services.Interface;

public interface ICategoryTypeServices
{
    public Task<(bool, CategoryType?)> GetByIdAsync(int? id);
    public Task<List<CategoryType>?> GetCategoryTypeListIncludingCategoriesAsync();

    public IQueryable<CategoryType?>? GetQueryableCategoryTypeContainsWord(string? search);
    public IQueryable<CategoryType?>? GetCategoryTypeAsQueryableIncludingCategories();
    public IQueryable<CategoryType?>? GetCategoryTypeLAsQueryableWithoutCategories();

    public Task<bool> AddCategoryTypeAsync(CategoryType? categoryType);

    public Task<bool> UpdateCategoryTypeAsync(CategoryType? categoryType);

    public Task<bool> DeleteAsync(int? id);
}