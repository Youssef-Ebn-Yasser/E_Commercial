namespace E_Commercial.Helper.Interface;

public interface IProductHelper
{
    public Task<ShowProductDetails?> GetProductByIdAsyncWithImagesAsync(int? productId);
    public Task<List<ShowAllProductsViewModel>?> GetProductsContainsAsync(string search);
    public IQueryable<ShowAllProductsViewModel>? GetAllProductWithImages();
    public Task<List<ShowAllProductsViewModel>?> GetAllProductsWithoutImagesAsync();
    public Task<UpdateProductViewModel?> FindProductWithImagesAsync(int? id);

    public Task<bool> CreateAsync(CreateProductViewModel? createProductViewModel);
    public Task<bool> UpdateAsync(UpdateProductViewModel? updateProductViewModel);
    public Task<bool> DeleteAsync(int? id);
}