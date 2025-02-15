namespace E_Commercial.Helper.Implementation;

public class CategoryHelper : ICategoryHelper
{
    #region   Fields
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly IQueryExecution _queryExecution;

    #endregion

    #region   Constructor
    public CategoryHelper(IMapper mapper, ICategoryService categoryService, IQueryExecution queryExecution)
    {
        _mapper = mapper;
        _categoryService = categoryService;
        _queryExecution = queryExecution;
    }
    #endregion

    #region   Handle Methods

    public async Task<List<ShowAllCategoryViewModel>?> GetCategoryContainsAsync(string search)
    {
        var result = _categoryService.GetQueryableCategoryContainsWord(search);
        var model = _mapper.ProjectTo<ShowAllCategoryViewModel>(result);

        return model is not null ? await model.ToListAsync() : null;
    }
    public List<ShowAllCategoryViewModel>? GetShowAllViewModelList()
    {
        string query = """ 
        SELECT count(Products.ProductId) NumberOfProduct, Categories.CategoryId, CategoryTypes.CategoryTypeName, CategoryTypes.CreatedDate, Categories.CategoryName
        FROM     CategoryTypes JOIN
                          Categories ON CategoryTypes.CategoryTypeId = Categories.CategoryTypeId FULL OUTER JOIN
                          Products ON Categories.CategoryId = Products.CategoryId
        				  group by Categories.CategoryId, CategoryTypes.CategoryTypeName, CategoryTypes.CreatedDate, Categories.CategoryName
       
        """;
        var result = _queryExecution.ExecuteQuery<ShowAllCategoryViewModel>(query).ToList();

        return result;
    }
    public IQueryable<ShowAllCategoryViewModel>? GetAllCategoryWithProducts()
    {
        var result = _categoryService.GetCategoriesAsQueryableIncludingProducts();
        var model = _mapper.ProjectTo<ShowAllCategoryViewModel>(result);

        return model;
    }
    public IQueryable<ShowAllCategoryViewModel>? GetAllCategoryWithoutProducts()
    {
        var result = _categoryService.GetCategoriesAsQueryableWithoutProducts();
        var model = _mapper.ProjectTo<ShowAllCategoryViewModel>(result);

        return model;
    }
    public async Task<UpdateCategoryViewModel?> FindCategoryAsync(int? id)
    {
        if (id == null)
            return null;

        var result = await _categoryService.GetByIdAsync(id);
        var category = _mapper.Map<UpdateCategoryViewModel>(result.Item2);

        return category;
    }

    public async Task<bool> CreateAsync(CreateCategoryViewModel? createCategoryViewModel)
    {
        if (createCategoryViewModel == null)
            return false;

        var categoryType = _mapper.Map<Category>(createCategoryViewModel);
        var result = await _categoryService.CreateAsync(categoryType);

        return result ? true : false;
    }

    public async Task<bool> UpdateAsync(UpdateCategoryViewModel? updateCategoryViewModel)
    {
        if (updateCategoryViewModel == null)
            return false;

        var isExist = await _categoryService.GetByIdAsync(updateCategoryViewModel.CategoryId);

        if (isExist.Item2 == null || isExist.Item1 == false)
            return false;


        var category = _mapper.Map(updateCategoryViewModel, isExist.Item2);
        var result = await _categoryService.UpdateAsync(category);

        return result ? true : false;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        if (id == null)
            return false;

        var isExist = await _categoryService.GetByIdAsync(id);

        if (isExist.Item2 == null || isExist.Item1 == false)
            return false;

        var result = await _categoryService.DeleteAsync(id);

        return result ? true : false;
    }
    #endregion
}