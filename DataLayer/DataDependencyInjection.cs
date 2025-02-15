namespace DataLayer;
public static class DataDependencyInjection
{
    public static IServiceCollection addDataDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUniteOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        return services;
    }
}