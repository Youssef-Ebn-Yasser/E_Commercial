namespace E_Commercial.ViewModel.Home;


public class ShowCategoryTypeWithAllRelated
{
    public int CategoryTypeId { get; set; }
    public string? CategoryTypeName { get; set; }
    public List<CategoryViewModel> Categories { get; set; } = new();
}

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public List<ProductViewModel> Products { get; set; } = new();
}

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDetails { get; set; }
    public decimal Price { get; set; }
    public int? AmountInStock { get; set; }
    public string? Brand { get; set; }
    public List<ProductImageViewModel> ProductImages { get; set; } = new();
}

public class ProductImageViewModel
{
    public int ProductImageId { get; set; }
    public string? Path { get; set; }
}