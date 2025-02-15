namespace DataLayer.Constraints;

public static class ProductConstraints
{
    public static ModelBuilder AddProductConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.HasIndex(e => e.ProductName);

            entity.Property(e => e.AmountInStock).HasDefaultValue(0).IsRequired();
            entity.Property(e => e.ProductName).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)").IsRequired();
            entity.Property(entity => entity.ProductDetails).HasMaxLength(500).IsRequired();
            entity.Property(entity => entity.Brand).HasMaxLength(100).IsRequired();
        });
        return modelBuilder;
    }
}