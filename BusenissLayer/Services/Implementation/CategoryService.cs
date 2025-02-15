namespace BusinessLayer.Services.Implementation;

public class CategoryService : ICategoryService
{
    #region   Fields
    private IUniteOfWork _uniteOfWork { get; }
    #endregion

    #region   Constructor
    public CategoryService(IUniteOfWork uniteOfWork)
    {
        _uniteOfWork = uniteOfWork;
    }
    #endregion

    #region   Handle Method
    public async Task<(bool, Category?)> GetByIdAsync(int? id)
    {
        var result = await _uniteOfWork.Repository<Category>().GetByIdAsync(id);

        return result == null ? (false, null) : (true, result);
    }
    public async Task<List<Category>?> GetCategoryListIncludingProductsAsync()
    {
        var categoryType = await _uniteOfWork.Repository<Category>()
                                                             .GetTableNoTracking()
                                                             .Include(c => c.products)
                                                             .ToListAsync();

        return categoryType;
    }

    public IQueryable<Category?>? GetCategoriesAsQueryableIncludingProducts()
    {
        var categoryType = _uniteOfWork.Repository<Category>()
                                                                                 .GetTableNoTracking()
                                                                                 .Include(c => c.products);

        return categoryType;
    }

    public IQueryable<Category?>? GetCategoriesAsQueryableWithoutProducts()
    {
        var categoryType = _uniteOfWork.Repository<Category>()
                                                           .GetTableNoTracking();

        return categoryType;
    }

    public IQueryable<Category?>? GetQueryableCategoryContainsWord(string? search)
    {
        var products = _uniteOfWork.Repository<Category>().GetTableNoTracking()
                                                                         .Where(c => c.CategoryName!.Contains(search!));

        return products;
    }


    public async Task<bool> CreateAsync(Category? category)
    {
        if (category == null)
            return false;
        var isExist = await _uniteOfWork.Repository<Category>()
                                                  .GetTableNoTracking()
                                                  .Where(c => c.CategoryName == category.CategoryName)
                                                  .AnyAsync();
        if (isExist) return false;

        await _uniteOfWork.Repository<Category>().AddAsync(category);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateAsync(Category? category)
    {
        if (category == null) return false;

        var isExistExcludeItSelf = await _uniteOfWork.Repository<Category>()
                                                         .GetTableNoTracking()
                                                         .Where(c => c.CategoryId != category.CategoryId
                                                           && c.CategoryName == category.CategoryName)
                                                         .AnyAsync();
        if (isExistExcludeItSelf) return false;

        var categoryExist = await GetByIdAsync(category.CategoryId);

        if (!categoryExist.Item1 || categoryExist.Item2 is null) return false;

        _uniteOfWork.Repository<Category>().Update(categoryExist.Item2);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        if (id == null)
            return false;

        var categoryExist = await GetByIdAsync(id);

        if (!categoryExist.Item1 || categoryExist.Item2 is null)
            return false;

        _uniteOfWork.Repository<Category>().Delete(categoryExist.Item2);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }
    #endregion
}