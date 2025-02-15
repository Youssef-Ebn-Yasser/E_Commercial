namespace E_Commercial.DependenciesInjection;

public static class MapperDependenciesInjection
{
    public static IServiceCollection AddAutoMapperDependencyInjection(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}