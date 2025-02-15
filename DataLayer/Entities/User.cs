using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities;

public class User : IdentityUser
{
    public string NationalId { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<Order>? Orders { get; set; }
}