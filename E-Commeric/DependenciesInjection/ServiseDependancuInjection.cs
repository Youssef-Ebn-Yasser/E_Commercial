namespace E_Commercial.DependenciesInjection;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddServiceDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IAccountHelper, AccountHelper>();
        services.AddTransient<IUserHelper, UserHelper>();
        services.AddTransient<IProductHelper, ProductHelper>();

        services.AddTransient<ICategoryTypeHelper, CategoryTypeHelper>();
        services.AddTransient<ICategoryHelper, CategoryHelper>();

        return services;
    }
}