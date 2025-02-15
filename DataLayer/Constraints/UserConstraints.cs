namespace DataLayer.Constraints;

public static class UserConstraints
{
    public static ModelBuilder ADDUserConstraints(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.NationalId).HasColumnType("char(14)");
            entity.Property(e => e.Address).HasColumnType("nVarchar(200)").IsRequired();
            entity.Property(e => e.Name).HasColumnType("nVarchar(100)");
            entity.Property(e => e.Type).HasColumnType("nVarchar(100)").HasDefaultValue("Client");
            entity.Property(e => e.PhoneNumber).HasColumnType("char(11)");

        });
        return modelBuilder;
    }
}