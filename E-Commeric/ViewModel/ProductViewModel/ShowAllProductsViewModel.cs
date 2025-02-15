namespace E_Commercial.ViewModel.ProductViewModel;

public class ShowAllProductsViewModel
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductDetails { get; set; } = string.Empty;
    public int? AmountInStock { get; set; }
    public decimal Price { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public string? TargetedGender { get; set; } = string.Empty;
    public List<string>? Paths { get; set; } = new List<string>();
    public string CategoryName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}