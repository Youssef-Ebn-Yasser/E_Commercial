namespace E_Commercial.Helper.Implementation;

public class CategoryTypeHelper : ICategoryTypeHelper
{
    #region   Fields
    private readonly IMapper _mapper;
    private readonly ICategoryTypeServices _categoryTypeServices;
    #endregion

    #region   Constructor
    public CategoryTypeHelper(IMapper mapper, ICategoryTypeServices categoryTypeServices)
    {
        _mapper = mapper;
        _categoryTypeServices = categoryTypeServices;
    }
    #endregion

    #region   Handle Methods
    public async Task<List<ShowAllCategoryTypeViewModel>?> GetCategoryTypeContainsAsync(string search)
    {
        var result = _categoryTypeServices.GetQueryableCategoryTypeContainsWord(search);
        var model = _mapper.ProjectTo<ShowAllCategoryTypeViewModel>(result);

        return model is not null ? await model.ToListAsync() : null;
    }
    public async Task<UpdateCategoryTypeViewModel?> FindCategoryTypeAsync(int? id)
    {
        if (id == null)
            return null;

        var result = await _categoryTypeServices.GetByIdAsync(id);
        var categoryType = _mapper.Map<UpdateCategoryTypeViewModel>(result.Item2);

        return categoryType;
    }
    public IQueryable<ShowAllCategoryTypeViewModel>? GetAllCategoryTypeWithCategories()
    {
        var result = _categoryTypeServices.GetCategoryTypeAsQueryableIncludingCategories();
        var model = _mapper.ProjectTo<ShowAllCategoryTypeViewModel>(result);

        return model;
    }
    public IQueryable<ShowAllCategoryTypeViewModel>? GetAllCategoryTypeWithoutCategories()
    {
        var result = _categoryTypeServices.GetCategoryTypeLAsQueryableWithoutCategories();
        var model = _mapper.ProjectTo<ShowAllCategoryTypeViewModel>(result);

        return model;
    }

    public async Task<bool> CreateAsync(CreateCategoryTypeViewModel? createCategoryTypeViewModel)
    {
        if (createCategoryTypeViewModel == null)
            return false;

        var categoryType = _mapper.Map<CategoryType>(createCategoryTypeViewModel);
        var result = await _categoryTypeServices.AddCategoryTypeAsync(categoryType);

        return result ? true : false;
    }

    public async Task<bool> UpdateAsync(UpdateCategoryTypeViewModel? updateCategoryTypeViewModel)
    {
        if (updateCategoryTypeViewModel == null)
            return false;

        var isExist = await _categoryTypeServices.GetByIdAsync(updateCategoryTypeViewModel.CategoryTypeId);

        if (isExist.Item2 == null || isExist.Item1 == false)
            return false;

        var categoryType = _mapper.Map(updateCategoryTypeViewModel, isExist.Item2);
        var result = await _categoryTypeServices.UpdateCategoryTypeAsync(categoryType);

        return result ? true : false;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        if (id == null)
            return false;

        var isExist = await _categoryTypeServices.GetByIdAsync(id);

        if (isExist.Item2 == null || isExist.Item1 == false)
            return false;

        var result = await _categoryTypeServices.DeleteAsync(id);

        return result ? true : false;
    }
    #endregion
}