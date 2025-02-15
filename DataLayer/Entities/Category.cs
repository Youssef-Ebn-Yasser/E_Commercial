namespace DataLayer.Entities;

public class Category
{
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; } =string.Empty;

    [ForeignKey(nameof(CategoryTypeId))]
    public int CategoryTypeId { get; set; }
    public CategoryType? CategoryType { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Product>?  products { get; set; }
}