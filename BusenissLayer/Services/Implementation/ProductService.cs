namespace BusinessLayer.Services.Implementation;

public class ProductService : IProductService
{
    #region   Fields
    IFileService _fileService;
    IUniteOfWork _uniteOfWork;
    private string _environment = string.Empty;
    private readonly string StartWith = "/Added/images/";
    #endregion

    #region   Constructor
    public ProductService(IUniteOfWork uniteOfWork, IFileService fileService)
    {
        _uniteOfWork = uniteOfWork;
        _fileService = fileService;
    }
    #endregion

    #region   Handle Methods
    public async Task<List<Product>?> GetProductListAsync()
    {
        var products = await _uniteOfWork.Repository<Product>()
                                                   .GetTableNoTracking()
                                                   .Include(p => p.Category)
                                                   .ToListAsync();

        return products;
    }
    public async Task<Product?> GetProductByIdAsyncWithIncludeImagesAsync(int? productId)
    {
        var isExist = await _uniteOfWork.Repository<Product>()
                                              .GetTableAsTracking()
                                              .Include(p => p.ProductImages)
                                              .FirstOrDefaultAsync(p => p.ProductId == productId);

        return isExist;
    }
    public async Task<Product?> GetProductByIdAsyncWithoutIncludeImagesAsync(int? productId)
    {
        var isExist = await _uniteOfWork.Repository<Product>()
                                              .GetTableAsTracking()
                                              .FirstOrDefaultAsync(p => p.ProductId == productId);

        return isExist;
    }

    public IQueryable<Product?>? GetQueryableProductsContainsWord(string? search)
    {
        var products = _uniteOfWork.Repository<Product>().GetTableNoTracking()
                                                                         .Where(p => p.ProductName!.Contains(search!));

        return products;
    }
    public IQueryable<Product?>? GetProductsAsQueryable()
    {
        var products = _uniteOfWork.Repository<Product>()
                                                                              .GetTableAsTracking()
                                                                              .Include(p => p.ProductImages);

        return products;
    }

    public async Task<bool> AddProductAsync(Product? product, string? currentDirectory)
    {
        if (currentDirectory is null || product is null) return false;

        var isExist = await _uniteOfWork.Repository<Product>()
                                            .GetTableNoTracking()
                                            .Where(p => p.ProductName == product.ProductName)
                                            .AnyAsync();
        if (isExist) return false;

        var productImages = new List<ProductImage>();
        var paths = new List<string>();
        _environment = currentDirectory;
        try
        {
            _uniteOfWork.BeginTransaction();
            await _uniteOfWork.Repository<Product>().AddAsync(product);
            _uniteOfWork.Complete();
            var result = await addProductImagesAsync(product.Files, product.ProductId);

            if (result.Item1 == null || result.Item2 == "failed")
                return false;

            paths = result.Item1;

            var complete = _uniteOfWork.Complete();

            if (complete > 0)
                _uniteOfWork.CommitTransaction();

            else
                _uniteOfWork.RollbackTransaction();

            return complete > 0 ? true : false;
        }
        catch
        {
            _uniteOfWork.RollbackTransaction();

            foreach (var path in paths)
            {
                _fileService.DeletePhysicalFile(path);
            }

            return false;
        }
    }


    public async Task<bool> UpdateProductAsync(Product? product)
    {
        if (product is null || product.ProductId is null) return false;

        var isExistExcludeItSelf = await _uniteOfWork.Repository<Product>()
                                                       .GetTableNoTracking()
                                                       .Where(p => p.ProductName != product.ProductName
                                                           && p.ProductName == product.ProductName)
                                                       .AnyAsync();
        if (isExistExcludeItSelf) return false;

        var getImages = _uniteOfWork.Repository<ProductImage>().GetTableAsTracking()
                                                                              .Where(img => img.ProductId == product.ProductId)
                                                                              .ToList();

        try
        {
            _uniteOfWork.BeginTransaction();

            _uniteOfWork.Repository<ProductImage>().DeleteRange(getImages);
            _uniteOfWork.Repository<Product>().Update(product);


            foreach (var image in getImages)
            {
                _fileService.DeletePhysicalFile(image.Path!);
            }

            if (product.Files != null)
            {
                foreach (var image in product.Files)
                {
                    var path = await _fileService.UploadAsync(image!, _environment);
                    var newProductImages = new ProductImage()
                    {
                        Path = path,
                        ProductId = product.ProductId,
                    };
                    await _uniteOfWork.Repository<ProductImage>().AddAsync(newProductImages);
                }
            }

            var result = _uniteOfWork.Complete();

            if (result > 0)
            {
                _uniteOfWork.CommitTransaction();
                return true;
            }

            _uniteOfWork.RollbackTransaction();
            return false;
        }
        catch
        {
            _uniteOfWork.RollbackTransaction();

            foreach (var image in product.ProductImages)
            {
                _fileService.DeletePhysicalFile(image.Path!);
            }

            return false;
        }
    }

    public async Task<bool> DeleteProductWithItsImagesAsync(int? id)
    {
        var isExist = await GetProductByIdAsyncWithIncludeImagesAsync(id);
        if (isExist == null)
            return false;

        try
        {
            _uniteOfWork.BeginTransaction();

            if (isExist.ProductImages.Count > 0)
            {

                _uniteOfWork.Repository<ProductImage>().DeleteRange(isExist.ProductImages);
                foreach (var images in isExist.ProductImages)
                {
                    _fileService.DeletePhysicalFile(images.Path!);
                }
            }
            _uniteOfWork.Complete();

            _uniteOfWork.Repository<Product>().Delete(isExist);

            _uniteOfWork.Complete();
            _uniteOfWork.CommitTransaction();

            return true;
        }
        catch
        {
            _uniteOfWork.RollbackTransaction();
            if (isExist.Files is not null)
            {
                foreach (var images in isExist.Files)
                {
                    await _fileService.UploadAsync(images!, _environment);
                }
            }

            return false;
        }
    }


    public async Task<bool> IsProductNameExistExcludeItselfAsync(string? name, int? id)
    {
        var isExist = await _uniteOfWork.Repository<Product>()
                                              .GetTableNoTracking()
                                              .Where(p => p.ProductName != name && p.ProductId == id)
                                              .FirstOrDefaultAsync();
        return isExist != null ? true : false;
    }
    public async Task<bool> IsProductNameExistAsync(string? productName)
    {
        var isExist = await _uniteOfWork.Repository<Product>()
                                              .GetTableNoTracking()
                                              .Where(p => p.ProductName == productName)
                                              .FirstOrDefaultAsync();
        return isExist != null ? true : false;
    }

    private async Task<(List<string>?, string)> addProductImagesAsync(List<Microsoft.AspNetCore.Http.IFormFile?>? files, int? productId)
    {
        List<string>? paths = new List<string>();

        if (files.IsNullOrEmpty())
            return (null, "failed");

        foreach (var file in files!)
        {
            var path = await _fileService.UploadAsync(file, _environment);
            if (!path.StartsWith(StartWith))
                return (null, "failed");

            paths.Add(path);
        }

        var productImages = new List<ProductImage>();

        foreach (var path in paths)
        {
            var productImage = new ProductImage()
            {
                Path = path,
                ProductId = productId,
            };

            productImages.Add(productImage);
        }
        await _uniteOfWork.Repository<ProductImage>().AddRangeAsync(productImages);

        return (paths, "Success");
    }
    #endregion
}