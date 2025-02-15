namespace DataLayer.Entities;

public class CategoryType
{
    public int? CategoryTypeId { get; set; }
    public string? CategoryTypeName { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Category>? Categories { get; set; }  = new List<Category>();
}