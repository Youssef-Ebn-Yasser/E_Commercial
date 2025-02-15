namespace E_Commercial.ViewModel.Home;

public class CartPageViewModel
{
    public int OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public int Discount { get; set; } = 0;
    public decimal Tax { get; set; }
    public int ProductCount { get; set; }
    public decimal AfterDiscount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<ProductDto>? productDto { get; set; }
}
public class ProductDto
{
    public int? ProductId { get; set; }
    public string Path { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public int Amount { get; set; } = 1;
}