using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataLayer;

public class ApplicationDbContext : IdentityDbContext<User>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public ApplicationDbContext() { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryType> CategoryTypes { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Order> order { get; set; }
    public DbSet<ProductOrder> productOrder { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddOrderConstraints()
                    .AddOrderConstraints()
                    .ADDCategoryConstraints()
                    .AddCategoryTypeConstraints()
                    .ADDUserConstraints()
                    .AddProductImageConstraints();

        base.OnModelCreating(modelBuilder);
    }
}