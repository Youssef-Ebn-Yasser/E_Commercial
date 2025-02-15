namespace E_Commercial.Helper.Implementation;

public class ProductHelper : IProductHelper
{
    #region   Fields
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IQueryExecution _queryExecution;
    private readonly IWebHostEnvironment _environment;
    private string folder = "Added/Images";
    #endregion

    #region   Constructor
    public ProductHelper(IMapper mapper, IProductService productService, IQueryExecution queryExecution, IWebHostEnvironment environment)
    {
        _mapper = mapper;
        _productService = productService;
        _queryExecution = queryExecution;
        _environment = environment;
    }
    #endregion

    #region   Handle Methods

    public async Task<List<ShowAllProductsViewModel>?> GetProductsContainsAsync(string search)
    {
        var result = _productService.GetQueryableProductsContainsWord(search);
        var model = _mapper.ProjectTo<ShowAllProductsViewModel>(result);

        return model is not null ? await model.ToListAsync() : null;
    }
    public async Task<ShowProductDetails?> GetProductByIdAsyncWithImagesAsync(int? productId)
    {
        if (productId == null)
            return null;

        var isExist = await _productService.GetProductByIdAsyncWithIncludeImagesAsync(productId);

        if (isExist == null)
            return null;

        var product = _mapper.Map<ShowProductDetails>(isExist);

        return product;
    }
    public IQueryable<ShowAllProductsViewModel>? GetAllProductWithImages()
    {
        var result = _productService.GetProductsAsQueryable();
        var model = _mapper.ProjectTo<ShowAllProductsViewModel>(result);

        return model;
    }
    public async Task<List<ShowAllProductsViewModel>?> GetAllProductsWithoutImagesAsync()
    {
        var result = await _productService.GetProductListAsync();
        var model = _mapper.Map<List<ShowAllProductsViewModel>>(result);

        return model;
    }
    public async Task<UpdateProductViewModel?> FindProductWithImagesAsync(int? id)
    {
        if (id == null)
            return null;

        var result = await _productService.GetProductByIdAsyncWithIncludeImagesAsync(id);
        var product = _mapper.Map<UpdateProductViewModel>(result);

        return product;
    }

    public async Task<bool> CreateAsync(CreateProductViewModel? createProductViewModel)
    {
        if (createProductViewModel == null)
            return false;

        var product = _mapper.Map<Product>(createProductViewModel);
        var result = await _productService.AddProductAsync(product, _environment.WebRootPath);

        return result ? true : false;
    }

    public async Task<bool> UpdateAsync(UpdateProductViewModel? updateProductViewModel)
    {
        if (updateProductViewModel == null)
            return false;

        var isExist = await _productService.GetProductByIdAsyncWithIncludeImagesAsync(updateProductViewModel.ProductId);

        if (isExist == null)
            return false;


        var product = _mapper.Map(updateProductViewModel, isExist);
        var result = await _productService.UpdateProductAsync(product);

        return result ? true : false;
    }

    public async Task<bool> DeleteAsync(int? id)
    {
        if (id == null)
            return false;

        var result = await _productService.DeleteProductWithItsImagesAsync(id);

        return result ? true : false;
    }

    #endregion
}