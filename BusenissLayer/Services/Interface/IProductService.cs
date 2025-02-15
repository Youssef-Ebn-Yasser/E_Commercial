namespace BusinessLayer.Services.Interface;

public interface IProductService
{
    public Task<List<Product>?> GetProductListAsync();
    public Task<Product?> GetProductByIdAsyncWithIncludeImagesAsync(int? productId);
    public Task<Product?> GetProductByIdAsyncWithoutIncludeImagesAsync(int? productId);

    public IQueryable<Product?>? GetProductsAsQueryable();
    public IQueryable<Product?>? GetQueryableProductsContainsWord(string? search);

    public Task<bool> AddProductAsync(Product? product, string? currentDirectory);

    public Task<bool> UpdateProductAsync(Product? product);
    public Task<bool> DeleteProductWithItsImagesAsync(int? id);

    public Task<bool> IsProductNameExistExcludeItselfAsync(string? name, int? id);
    public Task<bool> IsProductNameExistAsync(string? productName);
}