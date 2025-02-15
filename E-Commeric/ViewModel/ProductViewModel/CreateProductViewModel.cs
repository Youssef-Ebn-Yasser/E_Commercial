namespace E_Commercial.ViewModel.ProductViewModel;

public class CreateProductViewModel
{
    [Required(ErrorMessage = "Product Name is required")]
    public string? ProductName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Product Details is required")]
    public string? ProductDetails { get; set; } = string.Empty;
    [Required(ErrorMessage = "Product Price is required")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Amount In the Stock is required")]
    public int? AmountInStock { get; set; }
    [Required(ErrorMessage = "Brand is required")]
    public string? Brand { get; set; } = string.Empty;
    [Required(ErrorMessage = "Must enter At least one image")]
    public List<IFormFile?>? Files { get; set; }
    public int CategoryId { get; set; }
}