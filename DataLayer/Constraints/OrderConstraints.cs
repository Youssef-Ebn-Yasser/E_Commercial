namespace DataLayer.Constraints;

public static class OrderConstraints
{
    public static ModelBuilder AddOrderConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.Property(e => e.Discount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValue(DateTime.Now);

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(9,2)");
            entity.Property(e => e.AfterDiscount).HasComputedColumnSql("TotalPrice - ( Discount *.01 * TotalPrice)");
        });

        return modelBuilder;
    }
}