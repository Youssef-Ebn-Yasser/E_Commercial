namespace DataLayer.Entities;

public class ProductImage
{
    public int ProductImageId { get; set; }
    public string? Path { get; set; } = string.Empty;

    [ForeignKey(nameof(ProductId))]
    public int? ProductId { get; set; } 
    public Product? Product { get; set; } 
}