namespace DataLayer.Constraints;

public static class ProductImageConstraints
{
    public static ModelBuilder AddProductImageConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId);

            entity.Property(e => e.Path).HasMaxLength(500);
        });
        return modelBuilder;
    }
}