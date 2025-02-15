namespace DataLayer.Constraints;

public static class CategoryConstraints
{
    public static ModelBuilder ADDCategoryConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryName).HasMaxLength(100);

        });

        return modelBuilder;
    }
}