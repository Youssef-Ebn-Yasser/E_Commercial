namespace BusinessLayer.Services.Implementation;

public class CategoryTypeServices : ICategoryTypeServices
{
    #region   Fields
    private IUniteOfWork _uniteOfWork { get; }
    #endregion

    #region   Constructor
    public CategoryTypeServices(IUniteOfWork uniteOfWork)
    {
        _uniteOfWork = uniteOfWork;
    }
    #endregion

    #region   Handle Method
    public async Task<(bool, CategoryType?)> GetByIdAsync(int? id)
    {
        var result = await _uniteOfWork.Repository<CategoryType>().GetByIdAsync(id);

        return result == null ? (false, null) : (true, result);
    }
    public async Task<List<CategoryType>?> GetCategoryTypeListIncludingCategoriesAsync()
    {
        var categoryType = await _uniteOfWork.Repository<CategoryType>()
                                                                                    .GetTableNoTracking()
                                                                                    .Include(c => c.Categories)
                                                                                    .ToListAsync();

        return categoryType;
    }

    public IQueryable<CategoryType?>? GetQueryableCategoryTypeContainsWord(string? search)
    {
        var products = _uniteOfWork.Repository<CategoryType>().GetTableNoTracking()
                                                                         .Where(ct => ct.CategoryTypeName!.Contains(search!));

        return products;
    }
    public IQueryable<CategoryType?>? GetCategoryTypeAsQueryableIncludingCategories()
    {
        var categoryType = _uniteOfWork.Repository<CategoryType>()
                                                                                    .GetTableNoTracking()
                                                                                    .Include(c => c.Categories);

        return categoryType;
    }
    public IQueryable<CategoryType?>? GetCategoryTypeLAsQueryableWithoutCategories()
    {
        var categoryType = _uniteOfWork.Repository<CategoryType>()
                                                           .GetTableNoTracking();

        return categoryType;
    }

    public async Task<bool> AddCategoryTypeAsync(CategoryType? categoryType)
    {
        if (categoryType == null)
            return false;

        var isExist = await _uniteOfWork.Repository<CategoryType>()
                                            .GetTableNoTracking()
                                            .Where(ct => ct.CategoryTypeName == categoryType.CategoryTypeName)
                                            .AnyAsync();
        if (isExist) return false;

        await _uniteOfWork.Repository<CategoryType>().AddAsync(categoryType);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }

    public async Task<bool> UpdateCategoryTypeAsync(CategoryType? categoryType)
    {
        if (categoryType == null || categoryType.CategoryTypeId == null) return false;

        var isExistExcludeItSelf = await _uniteOfWork.Repository<CategoryType>()
                                                                   .GetTableNoTracking()
                                                                   .Where(ct => ct.CategoryTypeId != categoryType.CategoryTypeId
                                                                   && ct.CategoryTypeName == categoryType.CategoryTypeName)
                                                                   .AnyAsync();
        if (isExistExcludeItSelf) return false;

        var categoryTypeExist = await GetByIdAsync(categoryType.CategoryTypeId);

        if (!categoryTypeExist.Item1 || categoryTypeExist.Item2 is null)
            return false;

        _uniteOfWork.Repository<CategoryType>().Update(categoryType);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        if (id == null)
            return false;

        var categoryTypeExist = await GetByIdAsync(id);

        if (!categoryTypeExist.Item1 || categoryTypeExist.Item2 is null)
            return false;

        _uniteOfWork.Repository<CategoryType>().Delete(categoryTypeExist.Item2);
        var result = _uniteOfWork.Complete();

        return result > 0 ? true : false;
    }
    #endregion
}