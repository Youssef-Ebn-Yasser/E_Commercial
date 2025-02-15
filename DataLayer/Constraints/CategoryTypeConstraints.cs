namespace DataLayer.Constraints;

public static class CategoryTypeConstraints
{
    public static ModelBuilder AddCategoryTypeConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.HasKey(e => e.CategoryTypeId);

            entity.Property(e => e.CategoryTypeName).HasMaxLength(100).IsRequired();

        });
        return modelBuilder;
    }
}