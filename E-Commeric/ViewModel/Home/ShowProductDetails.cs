namespace E_Commercial.ViewModel.Home;

public class ShowProductDetails
{
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductDetails { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? AmountInStock { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public List<string>? Paths { get; set; }
}