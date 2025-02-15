namespace DataLayer.Entities;

public class ProductOrder
{
    public int Id { get; set; }
    public Order? Order { get; set; }
    [ForeignKey(nameof(OrderId))]
    public int? OrderId { get; set; }
    public Product? Product { get; set; }
    [ForeignKey(nameof(ProductId))]
    public int? ProductId { get; set; }
}