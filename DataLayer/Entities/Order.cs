namespace DataLayer.Entities;

public class Order
{
    public int OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public int Discount { get; set; }
    public int ProductCount { get; set; }
    public decimal AfterDiscount { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = new User();
}