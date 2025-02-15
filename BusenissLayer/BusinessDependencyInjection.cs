using BusinessLayer.Services.Implementation;

namespace BusinessLayer;

public static class BusinessDependencyInjection
{
    public static IServiceCollection addBusinessDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IQueryExecution, QueryExecution>();
        services.AddTransient<IGenericInformation, GenericInformation>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICategoryTypeServices, CategoryTypeServices>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IAccountServices, AccountServices>();
        services.AddTransient<IUserServices, UserServices>();

        return services;
    }
}