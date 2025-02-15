namespace DataLayer.Entities;

public class Product
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductDetails { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? AmountInStock { get; set; }
    public string? Brand { get; set; } = string.Empty;
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    [NotMapped]
    public List<IFormFile?>? Files { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}